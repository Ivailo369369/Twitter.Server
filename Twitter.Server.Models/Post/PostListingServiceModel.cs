namespace Twitter.Server.Models.Post
{
    public class PostListingServiceModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } 

        public int CommentsCount { get; set; } 

        public int LikesCount { get; set; }
    }
}
