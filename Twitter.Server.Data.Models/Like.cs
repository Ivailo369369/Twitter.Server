namespace Twitter.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Twitter.Server.Data.Models.Base;

    public class Like : Entity 
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }

        public int? CommentId { get; set; }

        public Comment Comment { get; set; }

        public bool Islike { get; set; }
    }
}
