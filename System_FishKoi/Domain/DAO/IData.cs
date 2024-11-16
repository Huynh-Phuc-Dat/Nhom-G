using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace System_FishKoi.Domain.DAO
{
    public interface IData
    {
        void Connect();
        void Disconnect();
        void AddParameter(string key, object value);
        void CreateNewStoredProcedure(string store);
        DataTable ExecStoreToDataTable();
        void ExecNonQuery();
        string ExecStoreToString();
        bool IsConnected();
        void BeginTransaction();
        void Commit();
        void RollBack();
    }

    public class Data : IData
    {
        private IDbConnection _dbConnection = null;
        private IDbTransaction _dbTransaction = null;
        private IDbCommand _dbCommand = null;

        private bool isBeginTransation = false;

        public Data()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["System_FishKoIDBContext"].ConnectionString;
            _dbConnection = new SqlConnection(connectionString);
        }

        public static Data CreateData()
        {
            return new Data();
        }

        public void AddParameter(string key, object value)
        {
            IDbDataParameter parameter = _dbCommand.CreateParameter();
            parameter.ParameterName = key;
            parameter.Value = value;
            _dbCommand.Parameters.Add(parameter);
        }

        public void Connect()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }

        public void CreateNewStoredProcedure(string store)
        {
            _dbCommand = new SqlCommand();
            _dbCommand = _dbConnection.CreateCommand();
            _dbCommand.CommandType = CommandType.StoredProcedure;
            _dbCommand.CommandText = store;

            if (isBeginTransation)
            {
                _dbCommand.Transaction = _dbTransaction;
            }
        }

        public void Disconnect()
        {
            _dbConnection.Close();
        }

        public DataTable ExecStoreToDataTable()
        {
            DataTable dataTable = new DataTable();
            try
            {
                DbDataAdapter adapter = new SqlDataAdapter((SqlCommand)_dbCommand);
                adapter.Fill(dataTable);
                _dbCommand.Parameters.Clear();
            }
            catch (Exception)
            {
                throw;
            }

            return dataTable;
        }

        public bool IsConnected()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }

        public void BeginTransaction()
        {
            isBeginTransation = true;
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void RollBack()
        {
            _dbTransaction.Rollback();
        }

        public void ExecNonQuery()
        {
            _dbCommand.ExecuteNonQuery();
            _dbCommand.Parameters.Clear();
        }

        public string ExecStoreToString()
        {
            string returnVal = "";
            _dbCommand.CommandTimeout = 1800;
            returnVal = _dbCommand.ExecuteScalar().ToString();

            _dbCommand.Parameters.Clear();
            return returnVal;
        }
    }
}