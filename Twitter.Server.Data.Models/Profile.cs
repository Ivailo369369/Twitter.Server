namespace Twitter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Server.Data.Models.Base; 

    using static Base.DataValidation.User;

    public class Profile : Entity
    {
        public Profile() => Follows = new List<Follow>();

        [Key]
        [Required]
        public string UserId { get; set; }

        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public string WebSite { get; set; }

        [MaxLength(MaxBiographyLength)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool IsPrivate { get; set; }

        public ICollection<Follow> Follows { get; set; }
    }
}
