namespace NCourses.Web.Models;

public class ClientSettings
{
    public Client WebClient { get; set; } = new();
    public Client WebClientForUser { get; set; } = new();

    public class Client
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}