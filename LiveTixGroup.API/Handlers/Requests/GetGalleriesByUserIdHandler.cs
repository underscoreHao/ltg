using MediatR;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Models.Responses;

namespace LiveTixGroup.API.Handlers.Requests;

public class GetGalleriesByUserIdHandler : IRequestHandler<GetGalleriesByUserId, GetGalleriesResponse>
{
	private readonly ILogger<GetGalleriesByUserIdHandler> _logger;
	private readonly IMediator _mediator;

	public GetGalleriesByUserIdHandler(ILogger<GetGalleriesByUserIdHandler> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	public async Task<GetGalleriesResponse> Handle(GetGalleriesByUserId request, CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Fetching all albums for user {request.UserId}");

		var response = await _mediator.Send(new GetGalleries(), cancellationToken);
		response.Albums = response.Albums.Where(x => x.UserId == request.UserId);

		return response;
	}
}
