namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Models.Like;
    using Twitter.Server.Service.ServicesType;

    public interface ILikeService : IService
    {
        Task<Result> LikePostAsync(LikeRequestModel model, string userId);

        Task<Result> UnlikePostAsync(LikeRequestModel model, string userId);

        Task<Result> LikeCommentAsync(LikeRequestModel model, string userId);

        Task<Result> UnLikeCommentAsync(LikeRequestModel model, string userId); 
    }
}
