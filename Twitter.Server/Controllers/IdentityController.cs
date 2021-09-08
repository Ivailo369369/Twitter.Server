namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Models.Identity;
    using Twitter.Server.Service.Contracts;

    using static Infrastructure.WebConstants;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identity;

        public IdentityController(IIdentityService identity) => this.identity = identity;

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.identity.RegisterAsync(model);

            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await this.identity.LoginAsync(model);

            return token;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllUsersRequestModel>> GetAll()
           => await this.identity.GetAllAsync();

        [HttpGet]
        [Route(Id)] 
        public async Task<IEnumerable<UserDetailsRequestModel>> Details(string userId)
           => await this.identity.DetailsAsync(userId);

        [HttpGet]
        [Route(nameof(GetAllUsersRoles))] 
        public async Task<IEnumerable<UsersRolesRequestModel>> GetAllUsersRoles()
           => await this.identity.GetAllUsersRolesAsync();

        [HttpPut]
        [Route(nameof(Ban))]
        public async Task<IActionResult> Ban(string userId)
        {
            await this.identity.BanAync(userId); 

            return Ok();
        }

        [HttpPut]
        [Route(nameof(UnBan))]
        public async Task<IActionResult> UnBan(string userId)
        {
            await this.identity.UnBanAsync(userId); 

            return Ok();
        }
    }
}
