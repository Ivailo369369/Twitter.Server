namespace Twitter.Server.Models.Identity
{
    using System;

    public class UserDetailsRequestModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
