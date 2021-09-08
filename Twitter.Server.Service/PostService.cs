namespace Twitter.Server.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Twitter.Server.Data;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Models.Comment;
    using Twitter.Server.Models.Post;
    using Twitter.Server.Service.Contracts;

    public class PostService : IPostService
    {
        private readonly TwitterDbContext dbContext;

        public PostService(TwitterDbContext dbContext) => this.dbContext = dbContext;

        public async Task<int> CreateAsync(CreatePostRequestModel model, string userId)
        {
            var post = new Post()
            {
                ImageUrl = model.ImageUrl, 
                Description = model.Description, 
                UserId = userId
            };

            await this.dbContext.Posts.AddAsync(post);
            await this.dbContext.SaveChangesAsync();

            return post.Id; 
        }

        public async Task<IEnumerable<PostListingServiceModel>> ByUserAsync(string userId, int postId)
              => await this.dbContext
            .Posts
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedOn)
             .Select(c => new PostListingServiceModel
             {
                 Id = c.Id,
                 ImageUrl = c.ImageUrl,
                 LikesCount = CountLikesPost(postId),
                 CommentsCount = CountCommentsPost(postId)
             })
             .ToListAsync();

        public async Task<PostDetailsServiceModel> DetailsAsync(int id, string userId)
            => await this.dbContext
                .Posts
                .Where(c => c.Id == id)
                .Select(c => new PostDetailsServiceModel
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    UserName = c.User.UserName,
                    Comments = CommentDetails(id, userId), 
                    CommentCountLikes = CountCommentLikes(id)
                })
                .FirstOrDefaultAsync();

        public async Task<Result> UpdateAsync(UpdatePostRequestModel model, string userId)
        {
            var post = await this.GetByIdAndByUserId(model.PostId, userId);  
            if (post == null)
            {
                return "This user cannot edit this post.";
            }

            post.Description = model.Description; 

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeleteAsync(int postId, string userId)
        {
            var post = await this.GetByIdAndByUserId(postId, userId);
            if (post == null)  
            {
                return "This user cannot delete this post.";
            }
             
            this.dbContext.Posts.Remove(post);

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Post> GetByIdAndByUserId(int id, string userId)
          => await this.dbContext
              .Posts
              .Where(c => c.Id == id && c.UserId == userId)
              .FirstOrDefaultAsync();

        public async Task<Result> RemoveComment(int postId, int commentId, string userId)
        {
            var comment = await this.dbContext
                .Comments
                .Where(c => c.Id == commentId && c.PostId == postId && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return "This user cannot delete this comment. ";
            }

            comment.IsDeleted = true;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        private int CountCommentsPost(int postId)
        {
            var comments = this.dbContext
                .Posts
                .Where(c => c.Id == postId)
                .FirstOrDefault();

            var count = comments
                .Comments
                .Count(); 

            return count;
        }

        private int CountLikesPost(int postId)
        {
            var likes = this.dbContext
                .Posts
                .Where(l => l.Id == postId)
                .FirstOrDefault();

            var count = likes
                .Likes
                .Count();

            return count; 
        }

        private CommentDetailsModel CommentDetails(int id, string userid) 
        {
            var comment = this.dbContext
                .Comments
                .Where(c => c.PostId == id && c.UserId == userid)
                .FirstOrDefault();

            var model = new CommentDetailsModel() 
            {
                Username = comment.User.UserName,
                Text = comment.Comments
            };

            return model;
        }

        private int CountCommentLikes(int id)
        {
            var likes = this.dbContext
                .Comments
                .Where(c => c.Id == id)
                .FirstOrDefault();

            var count = likes
                .Likes
                .Count;

            return count; 
        }
    }
}
