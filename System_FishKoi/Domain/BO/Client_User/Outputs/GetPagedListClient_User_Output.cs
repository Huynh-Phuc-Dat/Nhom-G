namespace System_FishKoi.Domain.BO.Client_User.Outputs
{
    public class GetPagedListClient_User_Output
    {
        public int ClientUserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int TotalRow { get; set; }
    }
}