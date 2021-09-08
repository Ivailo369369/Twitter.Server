namespace Twitter.Server.Models.Post
{
    using System.ComponentModel.DataAnnotations;

    using static ModelsValidation.Post; 

    public class CreatePostRequestModel
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
