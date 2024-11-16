namespace System_FishKoi.Domain.BO.SystemUser.Outputs
{
    public class GetSystemUser_Output
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}