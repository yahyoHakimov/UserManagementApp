namespace UserManagementApp.Server.Views
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
