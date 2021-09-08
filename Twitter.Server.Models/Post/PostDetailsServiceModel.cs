namespace Twitter.Server.Models.Post
{
    using Twitter.Server.Models.Comment;

    public class PostDetailsServiceModel : PostListingServiceModel
    {
        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public CommentDetailsModel Comments { get; set; } 

        public int CommentCountLikes { get; set; } 
    }
}
