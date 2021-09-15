namespace Twitter.Server.Service.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Models.Search;

    public interface ISearchService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> ProfilesAsync(string query);
    }
}
