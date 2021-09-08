namespace Twitter.Server.Models.Identity
{
    public class UsersRolesRequestModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public string Role { get; set; }
    }
}
