using FluentValidation;
using LiveTixGroup.API.Models.Requests;

namespace LiveTixGroup.API.Validators;

public class GetGalleriesByUserIdValidator : AbstractValidator<GetGalleriesByUserId>
{
	public GetGalleriesByUserIdValidator()
	{
		RuleLevelCascadeMode = CascadeMode.Stop;

		RuleFor(x => x.UserId)
			.NotNull()
			.NotEmpty()
			.GreaterThan(0);

		// TODO: We probably want to use .WithMessage() or .WithErrorCode() in production level code
	}
}
