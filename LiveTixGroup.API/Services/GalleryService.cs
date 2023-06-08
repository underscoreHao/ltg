using LiveTixGroup.API.Services.Dto;

namespace LiveTixGroup.API.Services;

public class GalleryService : IGalleryService
{
	// TODO: Ideally we'll read these from IOptions
	// In the interest of saving time I'm putting these here directly
	private const string PhotosPath = "/photos";
	private const string AlbumPath = "/albums";

	private readonly ILogger<GalleryService> _logger;
	private readonly HttpClient _httpClient;

	public GalleryService(ILogger<GalleryService> logger, HttpClient httpClient)
	{
		_logger = logger;
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<Photo>?> GetPhotos(CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Fetching all photos");

		HttpRequestMessage httpReqMsg = new(HttpMethod.Get, PhotosPath);
		var httpResponse = await _httpClient.SendAsync(httpReqMsg, cancellationToken);

		httpResponse.EnsureSuccessStatusCode();

		return await httpResponse.Content.ReadFromJsonAsync<IEnumerable<Photo>>(cancellationToken: cancellationToken);
	}

	public async Task<IEnumerable<Album>?> GetAlbums(CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Fetching all albums");

		HttpRequestMessage httpReqMsg = new(HttpMethod.Get, AlbumPath);
		var httpResponse = await _httpClient.SendAsync(httpReqMsg, cancellationToken);

		httpResponse.EnsureSuccessStatusCode();

		return await httpResponse.Content.ReadFromJsonAsync<IEnumerable<Album>>(cancellationToken: cancellationToken);
	}
}
