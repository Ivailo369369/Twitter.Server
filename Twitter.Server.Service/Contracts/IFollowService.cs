namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;

    public interface IFollowService
    {
        Task<Result> Follow(string userId, string followerId);

        Task<bool> IsFollower(string userId, string followerId);
    }
}
