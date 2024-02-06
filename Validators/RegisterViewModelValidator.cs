using FluentValidation;
using Ispitpredaja.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator() 
        {
            var regexFirstLastName = @"^\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)+$";

            RuleFor(x => x.FirstName.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("First name must not be empty.")
                .Matches(regexFirstLastName).WithMessage("First name must begin with capital letter (John)")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

            RuleFor(x => x.LastName.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Last name must not be empty.")
                .Matches(regexFirstLastName).WithMessage("Last name must begin with capital letter (Doe), and be between 3 and 50 characters long.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(x => x.Username.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Username must not be empty.")
                .Matches(@"^[A-ZČĆŠĐŽa-zčćžđš0-9\s]{4,20}$").WithMessage("Username must be at least 4 and between 20 characters long.");

            RuleFor(x => x.Email.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email must not be empty.")
                .EmailAddress().WithMessage("Email must be in the correct format (example: name@gmail.com).");

            RuleFor(x => x.Password.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Password must not be empty.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password must contain a capital letter, one symbole and a number, and be at least 8 characters long.");

            RuleFor(x => x.Address.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address must be inserted").When(x => !string.IsNullOrEmpty(x.Address.Value) || x.AddressNumber.Value != 0);

            RuleFor(x => x.AddressNumber.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address number must be inserted").When(x => !string.IsNullOrEmpty(x.Address.Value));
        }
    }
}
