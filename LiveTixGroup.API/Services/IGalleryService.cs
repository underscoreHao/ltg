using LiveTixGroup.API.Services.Dto;

namespace LiveTixGroup.API.Services;

public interface IGalleryService
{
	Task<IEnumerable<Photo>?> GetPhotos(CancellationToken cancellationToken);

	Task<IEnumerable<Album>?> GetAlbums(CancellationToken cancellationToken);
}
