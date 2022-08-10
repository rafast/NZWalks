using FluentValidation;

namespace NZWalks.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<Models.DTOs.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
