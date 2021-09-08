namespace Twitter.Server.Service.Contracts
{
    using System.Threading.Tasks;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Profile;

    public interface IProfileService
    {
        Task<ProfileServiceModel> ByUser(string userId, bool allInformation = false);

        Task<Result> Update(
            string userId,
            string email,
            string userName,
            string name,
            string mainPhotoUrl,
            string webSite,
            string biography,
            Gender gender,
            bool isPrivate);

        Task<bool> IsPublic(string userId);
    }
}
