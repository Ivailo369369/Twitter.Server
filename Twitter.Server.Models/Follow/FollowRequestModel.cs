namespace Twitter.Server.Models.Follow
{
    using System.ComponentModel.DataAnnotations;

    public class FollowRequestModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
