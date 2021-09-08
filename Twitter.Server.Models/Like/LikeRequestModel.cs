namespace Twitter.Server.Models.Like
{
    using System.ComponentModel.DataAnnotations;

    public class LikeRequestModel
    {
        [Required]
        public int Id { get; set; } 
    }
}
