namespace Twitter.Server.Data.Models.Base
{
    public static class DataValidation
    {
        public class User
        {
            public const int MaxNameLength = 40;
            public const int MaxBiographyLength = 150;
        }

        public class Comment
        {
            public const int MaxCommentLenght = 900;
        }

        public class Post
        {
            public const int MaxDescriptionLength = 2000;
        }
    }
}
