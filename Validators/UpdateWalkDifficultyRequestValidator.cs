using FluentValidation;

namespace NZWalks.Validators
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<Models.DTOs.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
