using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace System_FishKoi.Helper
{
    public static class CommonHelper
    {
        public static bool IsDevelopment()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PPL_Warehouse_Development"] == "Development" ? true : false;
            //return Environment.GetEnvironmentVariable("PPL_Warehouse_Development") == "Development" ? true : false;
        }

        public static long GetNewVerson()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }

        public static byte[] FromBase64String(this string base64String)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            var strBase64 = regex.Replace(base64String, string.Empty);
            return Convert.FromBase64String(strBase64);
        }

        public static ExcelWorkbook LoadWorkbookFromBytes(ExcelPackage package, byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                package.Load(stream);
                return package.Workbook;
            }
        }

        public static byte[] ExportFile(DataTable dtData, string title)
        {
            //excel
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            var excelPackage = new ExcelPackage();
            var excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            int row = 2;
            // header
            if (dtData != null && dtData.Rows.Count > 0)
            {
                ExcelRange rangeex = excelWorksheet.Cells[row, 1, row, dtData.Columns.Count];
                rangeex.Value = title;
                rangeex.Style.Font.Size = 16;
                rangeex.Style.Font.Bold = true;
                rangeex.Merge = true;
                rangeex.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rangeex.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                row += 2;
                var col = 0;
                foreach (DataColumn item in dtData.Columns)
                {
                    rangeex = excelWorksheet.Cells[row, ++col, row, col];
                    rangeex.Value = item.ColumnName;
                    rangeex.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rangeex.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rangeex.Style.Font.Bold = true;
                    if (item.DataType == typeof(DateTime))
                    {
                        excelWorksheet.Column(col).Style.Numberformat.Format = "dd/MM/yyyy";
                    }

                    if (item.DataType == typeof(decimal))
                    {
                        var results = dtData.AsEnumerable();
                        var temp = results.Where(x => x.Field<decimal?>(item) >= 1000);
                        if (temp != null && temp.Count() > 0)
                        {
                            excelWorksheet.Column(col).Style.Numberformat.Format = "#,##";
                        }
                        else
                        {
                            excelWorksheet.Column(col).Style.Numberformat.Format = @"";
                        }
                    }
                }

                //draw content
                excelWorksheet.Cells["A" + (row + 1)].LoadFromDataTable(dtData, false);
                row += dtData.Rows.Count;
                rangeex = excelWorksheet.Cells[4, 1, row, dtData.Columns.Count];
                rangeex.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rangeex.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rangeex.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rangeex.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                rangeex.AutoFitColumns();
            }
            return excelPackage.GetAsByteArray();
        }

        public static byte[] GenFileImport(DataTable dtData)
        {
            //excel
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            var excelPackage = new ExcelPackage();
            var excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            int row = 1;
            // header
            if (dtData != null && dtData.Columns.Count > 0)
            {
                ExcelRange rangeex = excelWorksheet.Cells[row, 1, row, dtData.Columns.Count];
                var col = 0;
                foreach (DataColumn item in dtData.Columns)
                {
                    rangeex = excelWorksheet.Cells[row, ++col, row, col];
                    rangeex.Value = item.ColumnName;
                }
            }
            return excelPackage.GetAsByteArray();
        }

        public static byte[] GenFileImport_V1(DataTable dtData)
        {
            //excel
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            var excelPackage = new ExcelPackage();
            var excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            int row = 1;
            // header
            if (dtData != null && dtData.Columns.Count > 0)
            {
                ExcelRange rangeex = excelWorksheet.Cells[row, 1, row, dtData.Columns.Count];
                var col = 0;
                foreach (DataColumn item in dtData.Columns)
                {
                    rangeex = excelWorksheet.Cells[row, ++col, row, col];
                    rangeex.Value = item.ColumnName;
                }
                excelWorksheet.Cells["A" + (row + 1)].LoadFromDataTable(dtData, false);
            }
            return excelPackage.GetAsByteArray();
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^0[3|5|7|8|9]\d{8,9}$");
            return regex.IsMatch(phoneNumber);
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }
    }
}