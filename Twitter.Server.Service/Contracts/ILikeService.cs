namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Models.Like;

    public interface ILikeService
    {
        Task<Result> LikePostAsync(LikeRequestModel model, string userId);

        Task<Result> UnlikePostAsync(LikeRequestModel model, string userId);

        Task<Result> LikeCommentAsync(LikeRequestModel model, string userId);

        Task<Result> UnLikeCommentAsync(LikeRequestModel model, string userId); 
    }
}
