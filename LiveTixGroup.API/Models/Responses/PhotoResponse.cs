namespace LiveTixGroup.API.Models.Responses;

public record PhotoResponse
{
	public int AlbumId { get; set; }

	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Url { get; set; } = string.Empty;

	public string ThumbnailUrl { get; set; } = string.Empty;
}
