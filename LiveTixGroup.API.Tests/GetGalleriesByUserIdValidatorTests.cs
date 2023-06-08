using System.Threading.Tasks;
using FluentAssertions;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Validators;
using Xunit;

namespace LiveTixGroup.Tests;

public class GetGalleriesByUserIdValidatorTests
{
    private readonly GetGalleriesByUserIdValidator _sut;

    public GetGalleriesByUserIdValidatorTests()
        => _sut = new GetGalleriesByUserIdValidator();

    [Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(99)]
	[InlineData(int.MaxValue)]
    public async Task GetGalleriesByUserIdValidator_ShouldReturn_IsValid(int userId)
    {
        // Arrange
		GetGalleriesByUserId request = new() { UserId = userId };

        // Act
        var result = await _sut.ValidateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
    public async Task GetGalleriesByUserIdValidator_ShouldReturn_IsNotValid(int userId)
    {
        // Arrange
		GetGalleriesByUserId request = new() { UserId = userId };

        // Act
        var result = await _sut.ValidateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
    }
}
