namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;

    public interface IFollowService
    {
        Task<Result> FollowAsync(string userId, string followerId);

        Task<bool> IsFollowerAsync(string userId, string followerId);
    } 
}
