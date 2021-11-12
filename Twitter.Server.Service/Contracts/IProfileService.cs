namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Profile;
    using Twitter.Server.Service.ServicesType;

    public interface IProfileService : IService
    {
        Task<ProfileServiceModel> ByUserAsync(string userId, bool allInformation = false);

        Task<Result> UpdateAsync(
            string userId,
            string email,
            string userName,
            string name,
            string mainPhotoUrl,
            string webSite,
            string biography,
            Gender gender,
            bool isPrivate);

        Task<bool> IsPublicAsync(string userId);
    }
}
