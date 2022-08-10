using FluentValidation;

namespace NZWalks.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<Models.DTOs.AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
