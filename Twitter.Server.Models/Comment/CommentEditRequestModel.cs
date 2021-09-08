namespace Twitter.Server.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class CommentEditRequestModel
    {
        public int Id { get; set; } 

        [Required]
        public string Text { get; set; }
    }
}
