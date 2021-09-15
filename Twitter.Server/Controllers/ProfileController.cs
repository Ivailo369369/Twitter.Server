namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Twitter.Server.Infrastructure.Service;
    using Twitter.Server.Models.Profile;
    using Twitter.Server.Service.Contracts;

    using static Infrastructure.WebConstants; 

    public class ProfileController : ApiController
    {
        private readonly IProfileService profile;
        private readonly IFollowService follows; 
        private readonly ICurrentUserService currentUser;

        public ProfileController(
            IProfileService profile,
            IFollowService follows,
            ICurrentUserService currentUser)
        {
            this.profile = profile;
            this.currentUser = currentUser;
            this.follows = follows; 
        }

        [HttpGet]
        public async Task<ProfileServiceModel> Mine()
          => await this.profile.ByUserAsync(this.currentUser.GetId(), allInformation: true);

        [HttpGet]
        [Route(Id)]
        public async Task<ProfileServiceModel> Details(string id)
        {
            var includeAllInformation = await this.follows
                .IsFollowerAsync(id, this.currentUser.GetId());

            if (!includeAllInformation)
            {
                includeAllInformation = await this.profile.IsPublicAsync(id);
            }

            return await this.profile.ByUserAsync(id, includeAllInformation);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.profile.UpdateAsync(
                userId,
                model.Email,
                model.UserName,
                model.Name,
                model.MainPhotoUrl,
                model.WebSite,
                model.Biography,
                model.Gender,
                model.IsPrivate);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
