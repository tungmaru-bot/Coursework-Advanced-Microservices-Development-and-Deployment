// UrlShortener.Api/Controllers/ShortenerController.cs

using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Models; // Đừng quên using model

[ApiController]
[Route("[controller]")]
public class ShortenerController : ControllerBase
{
    private static readonly List<ShortenedUrl> _urls = new List<ShortenedUrl>();

    // Endpoint 1: POST /shortener
    [HttpPost]
    public IActionResult ShortenUrl([FromBody] ShortenUrlRequest request)
    {
        // TODO:
        var shortCode = Guid.NewGuid().ToString().Substring(0, 6);

        var newUrl = new ShortenedUrl
        {
            Id = _urls.Count + 1,
            LongUrl = request.Url,
            ShortCode = shortCode,
            CreatedAt = DateTime.UtcNow
        };

        _urls.Add(newUrl);

        var shortenedUrl = $"{Request.Scheme}://{Request.Host}/r/{shortCode}";
        return Ok(new { ShortUrl = shortenedUrl });
    }

    // Endpoint 2: GET /r/{code}
    [HttpGet("/r/{code}")]
    public IActionResult RedirectToUrl(string code)
    {
        var url = _urls.FirstOrDefault(u => u.ShortCode == code);

        if (url == null)
        {
            return NotFound();
        }

        return Redirect(url.LongUrl);
    }
}

public class ShortenUrlRequest
{
    public string Url { get; set; } = string.Empty;
}