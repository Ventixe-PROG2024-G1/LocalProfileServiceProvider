namespace LocalProfileServiceProvider.Data.Models
{
    public class AllProfilesResponseRest
    {
        public List<ProfileResponseRest>? Profiles { get; set; } = new List<ProfileResponseRest>();

        public string? Error { get; set; }
    }
}