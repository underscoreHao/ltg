namespace LiveTixGroup.API.Models.Responses;

public class GetGalleriesResponse
{
	public IEnumerable<AlbumResponse> Albums { get; set; } = null!;
}
