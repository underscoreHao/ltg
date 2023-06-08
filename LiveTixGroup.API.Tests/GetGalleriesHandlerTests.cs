using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using NSubstitute;
using LiveTixGroup.API.Handlers.Requests;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Models.Responses;
using System.Threading;
using LiveTixGroup.API.Services;
using LiveTixGroup.API.Services.Dto;
using NSubstitute.ReturnsExtensions;

namespace LiveTixGroup.Tests;

public class GetGalleriesHandlerTests
{
	private readonly GetGalleriesHandler _sut;
	private readonly ILogger<GetGalleriesHandler> _loggerMock = Substitute.For<ILogger<GetGalleriesHandler>>();
	private readonly IGalleryService _galleryServiceMock = Substitute.For<IGalleryService>();

	public GetGalleriesHandlerTests()
		=> _sut = new GetGalleriesHandler(_loggerMock, _galleryServiceMock); 

    [Fact]
    public async Task Handler_ShouldReturn_GetGalleriesResponse()
    {
		// Arrange
		var fixture = new Fixture();
		var request = fixture.Create<GetGalleries>();
		var photos = fixture.Create<List<Photo>>();
		var albums = fixture.Create<List<Album>>();
		var response = fixture.Create<GetGalleriesResponse>();

		_galleryServiceMock.GetPhotos(Arg.Any<CancellationToken>()).Returns(photos);
		_galleryServiceMock.GetAlbums(Arg.Any<CancellationToken>()).Returns(albums);

		// Act
		var result = await _sut.Handle(request, default);

		// Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<GetGalleriesResponse>();
    }

    [Fact]
    public async Task Handler_WithoutPhotos_ShouldThrow()
    {
		// Arrange
		var fixture = new Fixture();
		var request = fixture.Create<GetGalleries>();
		var albums = fixture.Create<List<Album>>();
		var response = fixture.Create<GetGalleriesResponse>();

		_galleryServiceMock.GetPhotos(Arg.Any<CancellationToken>()).ReturnsNull();
		_galleryServiceMock.GetAlbums(Arg.Any<CancellationToken>()).Returns(albums);

		// Act
		var result = async () => await _sut.Handle(request, default);

		// Assert
		await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'photos')");
    }

    [Fact]
    public async Task Handler_WithoutAlbums_ShouldThrow()
    {
		// Arrange
		var fixture = new Fixture();
		var request = fixture.Create<GetGalleries>();
		var photos = fixture.Create<List<Photo>>();
		var response = fixture.Create<GetGalleriesResponse>();

		_galleryServiceMock.GetPhotos(Arg.Any<CancellationToken>()).Returns(photos);
		_galleryServiceMock.GetAlbums(Arg.Any<CancellationToken>()).ReturnsNull();

		// Act
		var result = async () => await _sut.Handle(request, default);

		// Assert
		await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'albums')");
    }
}
