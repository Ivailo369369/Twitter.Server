namespace Twitter.Server.Models
{
    public static class ModelsValidation
    {
        public class User
        {
            public const int MaxNameLength = 40;
            public const int MaxBiographyLength = 150;
        }

        public class Post 
        {
            public const int MaxDescriptionLength = 2000;
        } 
        
        public class Comment
        {
            public const int MaxCommentLenght = 900; 
        }
    }
}
