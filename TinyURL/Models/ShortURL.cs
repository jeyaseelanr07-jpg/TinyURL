namespace TinyURL.Models
{
    public class ShortUrl
    {
        // Unique identifier for the database (Primary Key)
        public int Id { get; set; }

        // The long, original link (e.g., https://www.google.com/search?q=blazor+tutorial)
        public string OriginalUrl { get; set; } = string.Empty;

        // The 6-character random code (e.g., "a1b2c3")
        public string ShortCode { get; set; } = string.Empty;

        // Requirement: Mark a URL as "Private" (5 points)
        // If true, this link should be hidden from the public list
        public bool IsPrivate { get; set; }

        // Requirement: Show total clicks per URL (5 points)
        // Increments every time someone visits the ShortCode redirect
        public int ClickCount { get; set; }

        // Optional: Timestamp for sorting (shows newest links first)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
