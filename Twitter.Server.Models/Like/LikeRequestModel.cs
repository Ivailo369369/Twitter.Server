namespace Twitter.Server.Models.Like
{
    using System.ComponentModel.DataAnnotations;

    public class LikeRequestModel
    {
        [Required]
        public int PostId { get; set; }  
    }
}
