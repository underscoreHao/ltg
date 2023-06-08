using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using NSubstitute;
using LiveTixGroup.API.Handlers.Requests;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Models.Responses;
using MediatR;
using System.Threading;

namespace LiveTixGroup.Tests;

public class GetGalleriesByUserIdHandlerTests
{
	private readonly GetGalleriesByUserIdHandler _sut;
	private readonly ILogger<GetGalleriesByUserIdHandler> _loggerMock = Substitute.For<ILogger<GetGalleriesByUserIdHandler>>();
	private readonly IMediator _mediatorMock = Substitute.For<IMediator>();

	public GetGalleriesByUserIdHandlerTests()
		=> _sut = new GetGalleriesByUserIdHandler(_loggerMock, _mediatorMock); 

    [Fact]
    public async Task Handler_ShouldReturn_GetGalleriesResponse()
    {
		// Arrange
		var fixture = new Fixture();
		var request = fixture.Create<GetGalleriesByUserId>();
		var response = fixture.Create<GetGalleriesResponse>();

		_mediatorMock.Send(Arg.Any<IRequest<GetGalleriesResponse>>(), Arg.Any<CancellationToken>())
			.Returns(response);
		
		// Act
		var result = await _sut.Handle(request, default);

		// Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<GetGalleriesResponse>();
    }
}
