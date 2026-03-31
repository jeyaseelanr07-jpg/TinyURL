using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace TinyUrlAPI.Data
{
        public class ShortUrl
        {
            public int Id { get; set; }
            public string OriginalUrl { get; set; } = "";
            public string ShortCode { get; set; } = "";
            public bool IsPrivate { get; set; }
            public int ClickCount { get; set; }
        }
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
        }
    public record UrlRequest(string Url, bool IsPrivate);
}
