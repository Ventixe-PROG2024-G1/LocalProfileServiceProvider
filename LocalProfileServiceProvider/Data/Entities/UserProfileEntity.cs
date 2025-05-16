using System.ComponentModel.DataAnnotations;

namespace LocalProfileServiceProvider.Data.Entities
{
    public class UserProfileEntity
    {
        [Key]
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string StreetAddress { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; }

        public string? Phone { get; set; }
    }
}