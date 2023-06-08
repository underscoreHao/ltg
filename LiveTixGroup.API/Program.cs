using System.Reflection;
using MediatR;
using FluentValidation;
using LiveTixGroup.API.Models.Requests;
using LiveTixGroup.API.Models.Responses;
using LiveTixGroup.API.Services;
using LiveTixGroup.API.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddScoped<IGalleryService, GalleryService>();

// HTTP Client
builder.Services.AddHttpClient<IGalleryService, GalleryService>((client) =>
{
	client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");
});

// MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Controllers
// I've decided to go with minimal API since the task doesn't really warrant full blown controllers
app.MapGet("/", () => "Hello LTG!");

app.MapGet("/galleries", async (IMediator mediator) => {
	return await mediator.Send(new GetGalleries()) is GetGalleriesResponse result
		? Results.Ok(result)
		: Results.BadRequest();
});

app.MapGet("/gallery/{userId}", async (int userId, IMediator mediator) => {
	return await mediator.Send(new GetGalleriesByUserId { UserId = userId }) is GetGalleriesResponse result
		? Results.Ok(result)
		: Results.BadRequest();
});

app.Run();

