using MediatR;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Models.Responses;
using LiveTixGroup.API.Services;
using LiveTixGroup.API.Services.Dto;

namespace LiveTixGroup.API.Handlers.Requests;

public class GetGalleriesHandler : IRequestHandler<GetGalleries, GetGalleriesResponse>
{
	private readonly ILogger<GetGalleriesHandler> _logger;
	private readonly IGalleryService _galleryService;

	public GetGalleriesHandler(ILogger<GetGalleriesHandler> logger, IGalleryService galleryService)
	{
		_logger = logger;
		_galleryService = galleryService;
	}

	public async Task<GetGalleriesResponse> Handle(GetGalleries request, CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Fetching all photos and albums");

		var photos = await _galleryService.GetPhotos(cancellationToken);
		var albums = await _galleryService.GetAlbums(cancellationToken);

		var (mappedPhotos, mappedAlbums) = MapResponses(photos, albums);

		var mergedAlbums = mappedAlbums.GroupJoin(mappedPhotos,
			a => a.Id,
			p => p.AlbumId,
			(a, p) => 
			{
				a.Photos = p.ToList();
				return a;
			});

		GetGalleriesResponse response = new()
		{
			Albums = mergedAlbums
		};

		return response;
	}

	private (IEnumerable<PhotoResponse>, IEnumerable<AlbumResponse>) MapResponses(IEnumerable<Photo>? photos, IEnumerable<Album>? albums)
	{
		if (photos is null)
			throw new ArgumentNullException("photos");

		if (albums is null)
			throw new ArgumentNullException("albums");

		var photosResponse = photos.Select(x => {
			return new PhotoResponse
			{ 
				AlbumId = x.AlbumId, 
				Id = x.Id,
				Title = x.Title,
				Url = x.Url,
				ThumbnailUrl = x.ThumbnailUrl
			};
		}).ToList();

		var albumResponse = albums.Select(x => {
			return new AlbumResponse
			{ 
				UserId = x.UserId, 
				Id = x.Id,
				Title = x.Title,
			};
		}).ToList();

		return (photosResponse, albumResponse);
	}
}
