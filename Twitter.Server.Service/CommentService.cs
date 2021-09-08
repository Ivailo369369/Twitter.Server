namespace Twitter.Server.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Comment;
    using Twitter.Server.Service.Contracts;

    public class CommentService : ICommentService
    {
        private readonly TwitterDbContext dbContext;

        public CommentService(TwitterDbContext dbContext) => this.dbContext = dbContext; 

        public async Task<Result> CommentAsync(CommentRequestModel model, string userId)
        { 
            var postObj = await this.dbContext
                .Posts
                .Where(p => p.Id == model.PostId) 
                .FirstOrDefaultAsync();

            if (postObj == null)
            {
                return "You can't comment this post because it doest exist!"; 
            }

            var comment = new Comment()
            {
                PostId = postObj.Id, 
                UserId = userId,
                Comments = model.Text
            };

            this.dbContext.Comments.Add(comment);

            postObj.Comments.Add(comment);

            await this.dbContext.SaveChangesAsync();

            return true; 
        }

        public async Task<Result> EditAsync(int postId, int commentId, string userId)
        {
            var commentObj = await this.dbContext
               .Comments
               .Where(c => c.PostId == postId && c.Id == commentId && c.UserId == userId)
               .FirstOrDefaultAsync();

            var model = new CommentEditRequestModel()  
            {
                Id = commentObj.Id,
                Text = commentObj.Comments
            };

            var comment = new Comment()
            {
                Id = model.Id, 
                Comments = model.Text
            };

            this.dbContext.Comments.Update(comment);

            await this.dbContext.SaveChangesAsync();

            return true; 
        }

        public async Task<Result> RemoveAsync(int postId, int commentId, string userId)
        {
            var comment = await this.dbContext
                .Comments
                .Where(c => c.PostId == postId && c.Id == commentId && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return "You can't delete this comment";
            }

            var result = comment.IsDeleted = true; 

            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
