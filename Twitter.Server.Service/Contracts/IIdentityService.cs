namespace Twitter.Server.Service.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Models.Identity;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        Task RegisterAsync(RegisterRequestModel model);

        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);

        Task<IEnumerable<GetAllUsersRequestModel>> GetAllAsync();

        Task<IEnumerable<UserDetailsRequestModel>> DetailsAsync(string userId);

        Task<IEnumerable<UsersRolesRequestModel>> GetAllUsersRolesAsync();

        Task BanAync(string userId);

        Task UnBanAsync(string userId);
    }
}
