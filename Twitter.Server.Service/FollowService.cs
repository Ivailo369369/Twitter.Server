namespace Twitter.Server.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Service.Contracts;

    public class FollowService : IFollowService
    {
        private readonly TwitterDbContext dbContext;

        public FollowService(TwitterDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Result> FollowAsync(string userId, string followerId)
        {
            var userAlreadyFollowed = await this.dbContext
                .Follows
                .AnyAsync(f => f.UserId == userId && f.FollowerId == followerId);

            if (userAlreadyFollowed)
            {
                return "This user is already followed.";
            }

            var publicProfile = await this.dbContext
                .Profiles
                .Where(p => p.UserId == userId)
                .Select(p => !p.IsPrivate)
                .FirstOrDefaultAsync();

            this.dbContext.Follows.Add(new Follow
            {
                UserId = userId,
                FollowerId = followerId,
                IsApproved = publicProfile
            });

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsFollowerAsync(string userId, string followerId)
            => await this.dbContext
                .Follows
                .AnyAsync(f => f.UserId == userId &&
                    f.FollowerId == followerId &&
                    f.IsApproved);
    }
}
