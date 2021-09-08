namespace Twitter.Server.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Models.Search;
    using Twitter.Server.Service.Contracts;

    public class SearchService : ISearchService
    {
        private readonly TwitterDbContext dbContext;

        public SearchService(TwitterDbContext dbContext) => this.dbContext = dbContext; 

        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.dbContext
                .Users
                .Where(u => u.UserName.ToLower().Contains(query.ToLower()) ||
                    u.Profile.Name.ToLower().Contains(query.ToLower()))
                .Select(u => new ProfileSearchServiceModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    ProfilePhotoUrl = u.Profile.MainPhotoUrl
                })
                .ToListAsync();
    }
}
