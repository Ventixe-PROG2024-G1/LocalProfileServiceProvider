namespace LocalProfileServiceProvider.Data.Models
{
    public class ActionResponseRest
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
    }
}