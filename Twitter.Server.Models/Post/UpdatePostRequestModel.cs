namespace Twitter.Server.Models.Post
{
    using System.ComponentModel.DataAnnotations;

    using static ModelsValidation.Post; 

    public class  UpdatePostRequestModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } 
    }
}
