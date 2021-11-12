namespace Twitter.Server.Service.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Models.Search;
    using Twitter.Server.Service.ServicesType;

    public interface ISearchService : IService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> ProfilesAsync(string query);
    }
}
