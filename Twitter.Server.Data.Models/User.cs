namespace Twitter.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using Twitter.Server.Data.Models.Base;

    public class User : IdentityUser, IEntity
    {
        public User() => Posts = new List<Post>();

        public Profile Profile { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
