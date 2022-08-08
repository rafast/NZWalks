﻿using System;
using FluentValidation;

namespace NZWalks.Validators
{
	public class LoginRequestValidator : AbstractValidator<Models.DTOs.LoginRequest>
	{
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
	}
}
