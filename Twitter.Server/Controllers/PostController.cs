namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twitter.Server.Infrastructure.Service;
    using Twitter.Server.Models.Post;
    using Twitter.Server.Service.Contracts;

    using static Infrastructure.WebConstants; 

    public class PostController : ApiController
    {
        private readonly IPostService post;
        private readonly ICurrentUserService currentUser;

        public PostController(
            IPostService post, 
            ICurrentUserService currentUser)
        {
            this.post = post;
            this.currentUser = currentUser;
        } 

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequestModel model)
        {
            var userId = this.currentUser.GetId(); 

            var id = await this.post.CreateAsync(model, userId);

            return Created(nameof(this.Create), id);
        }

        [HttpGet]
        public async Task<IEnumerable<PostListingServiceModel>> Mine(int postId)
            => await this.post.ByUserAsync(this.currentUser.GetId(), postId);  

        [HttpGet]
        [Route(Id)]
        public async Task<PostDetailsServiceModel> Details(int id)
        {
            var userId = this.currentUser.GetId();

            var model = await this.post.DetailsAsync(id, userId);

            return model;
        }

        [HttpPut]
        [Route(Id)]
        public async Task<IActionResult> Update(UpdatePostRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.post.UpdateAsync(model, userId);

            if (result.Failure)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.currentUser.GetId();

            var result = await this.post.DeleteAsync(id, userId); 
            if (result.Failure)
            {
                return BadRequest();
            }

            return Ok();
        } 

        [HttpDelete]
        [Route(nameof(RemoveComment))] 
        public async Task<IActionResult> RemoveComment(int postId, int commentId)
        {
            var userId = this.currentUser.GetId();

            var result = await this.post.RemoveComment(postId, commentId, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(); 
        }
    }
}
