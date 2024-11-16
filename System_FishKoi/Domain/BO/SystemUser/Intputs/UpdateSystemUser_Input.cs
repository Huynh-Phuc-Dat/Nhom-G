namespace System_FishKoi.Domain.BO.SystemUser.Intputs
{
    public class UpdateSystemUser_Input
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}