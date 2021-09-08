namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Models.Comment;

    public interface ICommentService
    {
        Task<Result> CommentAsync(CommentRequestModel model, string userId);

        Task<Result> RemoveAsync(int postId, int commentId, string userId);

        Task<Result> EditAsync(int postId, int commentId, string userId); 
    }
}
