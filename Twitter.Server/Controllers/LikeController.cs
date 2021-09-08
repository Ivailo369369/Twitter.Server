namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Twitter.Server.Infrastructure.Service;
    using Twitter.Server.Models.Like;
    using Twitter.Server.Service.Contracts;

    public class LikeController : ApiController
    {
        private readonly ILikeService like;
        private readonly ICurrentUserService currentUser; 

        public LikeController(
            ILikeService like, 
            ICurrentUserService currentUser)
        {
            this.like = like;
            this.currentUser = currentUser; 
        } 

        [HttpPut] 
        [Route(nameof(LikePost))]
        public async Task<IActionResult> LikePost(LikeRequestModel model) 
        {
            var userId = this.currentUser.GetId(); 

            var result =  await this.like.LikePostAsync(model, userId);  

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(); 
        } 

        [HttpPut] 
        [Route(nameof(UnLikePost))]
        public async Task<IActionResult> UnLikePost(LikeRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.like.UnlikePostAsync(model, userId);
              
            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(); 
        } 

        [HttpPut]
        [Route(nameof(LikeComment))]
        public async Task<IActionResult> LikeComment(LikeRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.like.LikeCommentAsync(model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok();
        } 

        [HttpPut] 
        [Route(nameof(UnLikeComment))]
        public async Task<IActionResult> UnLikeComment(LikeRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.like.UnLikeCommentAsync(model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok();
        }
    }
}
