namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Models.Comment;
    using Twitter.Server.Service.ServicesType;

    public interface ICommentService : IService
    {
        Task<Result> CreateAsync(CommentRequestModel model, string userId); 

        Task<Result> RemoveAsync(int postId, int commentId, string userId);

        Task<Result> EditAsync(int postId, int commentId, string userId); 
    }
}
