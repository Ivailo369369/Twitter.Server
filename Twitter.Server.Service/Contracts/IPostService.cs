namespace Twitter.Server.Service.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Models.Post;

    public interface IPostService
    {
        Task<int> CreateAsync(CreatePostRequestModel model, string userId);

        Task<Result> UpdateAsync(UpdatePostRequestModel model, string userId);

        Task<Result> DeleteAsync(int postId, string userId);

        Task<IEnumerable<PostListingServiceModel>> ByUserAsync(string userId, int postId); 

        Task<PostDetailsServiceModel> DetailsAsync(int id, string userId);

        Task<Result> RemoveCommentAync(int postId, int commentId, string userId);
    }
}
