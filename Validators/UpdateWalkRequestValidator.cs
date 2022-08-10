﻿using FluentValidation;

namespace NZWalks.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<Models.DTOs.UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
