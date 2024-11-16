namespace System_FishKoi.Domain.BO.SystemUser.Outputs
{
    public class GetPagedListSystemUser_Output
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int TotalRow { get; set; }
    }
}