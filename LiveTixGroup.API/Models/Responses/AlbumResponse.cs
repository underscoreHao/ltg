namespace LiveTixGroup.API.Models.Responses;

public record AlbumResponse
{
	public int UserId { get; set; }

	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public IEnumerable<PhotoResponse> Photos { get; set; } = null!;
}
