namespace Twitter.Server.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Like;
    using Twitter.Server.Service.Contracts;

    public class LikeService : ILikeService
    {
        private readonly TwitterDbContext dbContext;

        public LikeService(TwitterDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Result> LikePostAsync(LikeRequestModel model, string userId)
        {
            var postObj = await this.dbContext
                .Posts
                .FirstOrDefaultAsync(p => p.Id == model.PostId);

            var likedPost = await this.dbContext
                .Likes
                .AnyAsync(l => l.PostId == postObj.Id && l.UserId == userId);     

            if (likedPost)
            {
                return "This post is already liked";
            }

            var like = new Like()
            {
                PostId = postObj.Id,
                UserId = userId,
                Islike = true
            };

            this.dbContext.Likes.Add(like);

            postObj.Likes.Add(like); 

            await this.dbContext.SaveChangesAsync();

            return true; 
        }

        public async Task<Result> UnlikePostAsync(LikeRequestModel model, string userId)  
        {
            var postObj = await this.dbContext
                .Posts
                .FirstOrDefaultAsync(p => p.Id == model.PostId);
                
            var likedPost = await this.dbContext
                .Likes
                .FirstOrDefaultAsync(l => l.PostId == postObj.Id && l.UserId == userId); 

            if (likedPost == null)
            {
                return "You can't unlike this post, because you didn't like it";
            }

            var unLike = likedPost.Islike = false;

            this.dbContext.Update(unLike);

            postObj.Likes.Remove(likedPost); 

            await this.dbContext.SaveChangesAsync();

            return true; 
        }

        public async Task<Result> LikeCommentAsync(LikeRequestModel model, string userId)
        {
            var commentObj = await this.dbContext
                .Comments
                .FirstOrDefaultAsync(c => c.Id == model.PostId);

            var likedComment = await this.dbContext
                .Likes
                .FirstOrDefaultAsync(l => l.CommentId == model.PostId && l.UserId == userId);

            if (likedComment != null)
            {
                return "This comment is already liked.";
            }

            var like = new Like()
            {
                CommentId = commentObj.Id, 
                UserId = userId
            };

            this.dbContext.Likes.Add(like);

            commentObj.Likes.Add(like);

            await this.dbContext.SaveChangesAsync(); 

            return true; 
        }

        public async Task<Result> UnLikeCommentAsync(LikeRequestModel model, string userId)
        {
            var commentObj = await this.dbContext
                .Comments
                .FirstOrDefaultAsync(c => c.Id == model.PostId);

            var likedComment = await this.dbContext
                .Likes
                .FirstOrDefaultAsync(l => l.PostId == commentObj.Id && l.UserId == userId);

            if (likedComment == null)
            {
                return "You can't unlike this post, because you didn't like it";
            }

            var unlikePost = likedComment.Islike = false;

            this.dbContext.Update(unlikePost); 

            commentObj.Likes.Remove(likedComment);

            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
