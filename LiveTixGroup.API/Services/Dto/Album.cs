using System.Text.Json.Serialization;

namespace LiveTixGroup.API.Services.Dto;

public record Album
{
	[JsonPropertyName("userId")]
	public int UserId { get; set; }

	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;
}
