namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Twitter.Server.Infrastructure.Service;
    using Twitter.Server.Models.Follow;
    using Twitter.Server.Service.Contracts;

    public class FollowController : ApiController
    {
        private readonly IFollowService follows;
        private readonly ICurrentUserService currentUser;

        public FollowController(
            IFollowService follows,
            ICurrentUserService currentUser)
        {
            this.follows = follows;
            this.currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(FollowRequestModel model) 
        {
            var result = await this.follows.Follow(
                model.UserId,
                this.currentUser.GetId());

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
