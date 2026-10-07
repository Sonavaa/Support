 namespace SupportApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PassHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
