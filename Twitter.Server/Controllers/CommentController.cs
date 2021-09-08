namespace Twitter.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Twitter.Server.Infrastructure.Service;
    using Twitter.Server.Models.Comment;
    using Twitter.Server.Service.Contracts;

    using static Infrastructure.WebConstants;

    public class CommentController : ApiController
    {
        private readonly ICommentService comment;
        private readonly ICurrentUserService currentUser; 

        public CommentController(
            ICommentService comment,
            ICurrentUserService currentUser)
        {
            this.comment = comment;
            this.currentUser = currentUser; 
        }  

        [HttpPut]
        public async Task<IActionResult> Comment(CommentRequestModel model)
        {
            var userId = this.currentUser.GetId(); 
             
            var result = await this.comment.CommentAsync(model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(); 
        }  

        [HttpPut] 
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(int postId, int commentId)
        {
            var userId = this.currentUser.GetId();

            var result = await this.comment.EditAsync(postId, commentId, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(); 
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<IActionResult> Remove(int postId, int commentId)
        {
            var userId = this.currentUser.GetId();

            var result = await this.comment.RemoveAsync(postId, commentId, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
