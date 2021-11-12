namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Service.ServicesType;

    public interface IFollowService : IService
    {
        Task<Result> FollowAsync(string userId, string followerId);

        Task<bool> IsFollowerAsync(string userId, string followerId);
    } 
}
