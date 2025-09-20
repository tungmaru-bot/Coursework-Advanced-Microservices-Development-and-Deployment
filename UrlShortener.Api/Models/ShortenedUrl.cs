// UrlShortener.Api/Models/ShortenedUrl.cs

public class ShortenedUrl
{
    public int Id { get; set; } // Primary Key
    public string LongUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}