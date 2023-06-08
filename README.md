# ltg

- Minimal .NET API with controllers and DI setup inside `Program.cs`.
- Usage of MediatR (requests and behavior pipeline) + FluentFalidation.
- Service layer dealing with 3rd party API needed for our data.
- Unit tests around handlers, errors and validation by using xUnit, NSubstitute, FluentAssertions and AutoFixture.

### Minimal API

I went with minimal API setup since it's 'quick to spin up and easy to read (if you're a reviewer). I also wanted to showcase knowledge on some of the newer features of .NET. Of course, on a bigger project I would've went with the normal ASP.NET controller setup, but for this task it seemed redundant.

The two endpoints are:

- `/galleries` for the merge operation
- `/gallery/{userId}` for filtering by userId

I've put everything in a single project in the interest of saving time. I also haven't added Swagger or anything else too fancy. Usually I'll have different projects for different layers like services, infrastructure, domain etc.

I've added basic tests around the major components of the API.

### TODO

Things that I would do if I had more time

- Use `IOptions<T>` for configuration setup.
- Addition of smoke and/or integration tests.
- Add an exception middleware that catches and wraps errors nicely

