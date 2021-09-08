namespace Twitter.Server.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Identity;
    using Twitter.Server.Service.Contracts;

    using static Constants.Constant; 

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;
        private readonly TwitterDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager; 

        public IdentityService(
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            TwitterDbContext dbContext,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.dbContext = dbContext;
            this.roleManager = roleManager; 
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }), 
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            }; 

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        //Better logic needs to be made to add a role to users 
        public async Task RegisterAsync(RegisterRequestModel model)
        {
            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email
            };

            if (await this.roleManager.RoleExistsAsync(RoleName))
            {
                await this.userManager.CreateAsync(user, model.Password);
                await this.userManager.AddToRoleAsync(user, RoleName); 
            }

            var role = new IdentityRole()
            {
                Name = RoleName
            };

            await this.roleManager.CreateAsync(role);

            await this.userManager.CreateAsync(user, model.Password);
            await this.userManager.AddToRoleAsync(user, role.Name); 
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                throw new ArgumentException("There is no such user");
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                throw new ArgumentException("Username or password is incorrect.");
            }

            var token = GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponseModel()
            {
                Token = token
            };
        }

        public async Task<IEnumerable<GetAllUsersRequestModel>> GetAllAsync()
           => await this.dbContext
            .Users
            .Select(u => new GetAllUsersRequestModel()
            {
                Id = u.Id,
                Username = u.UserName,
            })
            .ToListAsync();

        public async Task<IEnumerable<UserDetailsRequestModel>> DetailsAsync(string userId)
           => await this.dbContext
            .Users
            .Where(u => u.Id == userId)
            .Select(u => new UserDetailsRequestModel()
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                CreatedOn = u.CreatedOn
            })
            .ToListAsync();

        public async Task<IEnumerable<UsersRolesRequestModel>> GetAllUsersRolesAsync()
           => await (from user in dbContext
         .Users
                     join userRoles in dbContext
                      .UserRoles on user.Id equals userRoles.UserId
                     join role in dbContext.Roles on userRoles.RoleId equals role.Id
                     select new UsersRolesRequestModel
                     {
                         Id = user.Id,
                         Username = user.UserName,
                         Email = user.Email,
                         EmailConfirmed = user.EmailConfirmed,
                         IsLocked = user.LockoutEnabled,
                         Role = role.Name
                     })
          .ToListAsync();

        public async Task BanAync(string userId)
        {
            var user = await this.userManager
                .FindByIdAsync(userId); 

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.Now.AddDays(14); 

            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnBanAsync(string userId)
        {
            var user = await this.userManager
                .FindByIdAsync(userId); 

            user.LockoutEnabled = false; 

            await this.dbContext.SaveChangesAsync();
        }
    }
}
