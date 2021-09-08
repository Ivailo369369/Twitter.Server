namespace Twitter.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Twitter.Server.Data.Models.Base;

    public class Follow : Entity
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public User Follower { get; set; }

        public bool IsApproved { get; set; }
    }
}
