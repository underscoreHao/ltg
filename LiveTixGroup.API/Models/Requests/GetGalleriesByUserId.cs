using MediatR;
using LiveTixGroup.API.Models.Responses;

namespace LiveTixGroup.API.Models.Requests;

public class GetGalleriesByUserId : IRequest<GetGalleriesResponse>
{
	public int UserId { get; set; } 
}
