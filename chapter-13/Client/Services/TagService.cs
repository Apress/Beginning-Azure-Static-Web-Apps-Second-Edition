using System.Net.Http.Json;

namespace Client.Services;

public class TagService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly BlogpostService _blogpostService;
    public List<string> Tags { get; private set; } = [];
    public TagService(
    HttpClient httpClient,

    BlogpostService blogpostService)
    {
        _httpClient = httpClient;
        _blogpostService = blogpostService;
        _blogpostService.BlogpostChanged +=
        OnBlogpostsChanged;
    }

    public async Task LoadTags(bool forceLoad = false)
    {
        if (!Tags.Any() || forceLoad)
        {
            Tags = await
            _httpClient.GetFromJsonAsync<List<string>>("api/tags")
              ??
            [];
        }
    }

    public void Dispose()
    {
        _blogpostService.BlogpostChanged -= OnBlogpostsChanged;
    }

    private async void
      OnBlogpostsChanged(object? sender, EventArgs e)
    {
        await LoadTags(true);
    }
}
