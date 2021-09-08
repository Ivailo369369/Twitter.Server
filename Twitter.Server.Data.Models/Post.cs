namespace Twitter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Server.Data.Models.Base; 

    using static Base.DataValidation.Post; 

    public class Post : DeletableEntity
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
