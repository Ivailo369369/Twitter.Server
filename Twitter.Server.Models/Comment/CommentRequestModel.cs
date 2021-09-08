namespace Twitter.Server.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static ModelsValidation.Comment; 

    public class CommentRequestModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(MaxCommentLenght)]
        public string Text { get; set; }
    } 
}
