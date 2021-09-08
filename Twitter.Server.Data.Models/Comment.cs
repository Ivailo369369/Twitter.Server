namespace Twitter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Server.Data.Models.Base; 

    using static Base.DataValidation.Comment; 

    public class Comment : DeletableEntity
    {
        public Comment() => Likes = new List<Like>();

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [MaxLength(MaxCommentLenght)]
        public string Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
