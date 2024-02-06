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
    public class UpdateRestaurantViewModelValidator : AbstractValidator<UpdateRestaurantViewModel>
    {
        public UpdateRestaurantViewModelValidator()
        {
            RuleFor(x => x.Name.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name must not be empty.")
                .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
                .MaximumLength(50).WithMessage("Name must have at least 50 characters.").When(x => !string.IsNullOrEmpty(x.Name.Value));

            RuleFor(x => x.Description.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description must not be empty.")
                .MinimumLength(10).WithMessage("Description must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Description can be 200 characters long.").When(x => !string.IsNullOrEmpty(x.Description.Value));

            RuleFor(x => x.Address.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address must not be empty.")
                .MinimumLength(5).WithMessage("Address must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Address can be 200 characters long.").When(x => !string.IsNullOrEmpty(x.Address.Value));

            RuleFor(x => x.AddressNumber.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address number must not be empty.")
                .GreaterThanOrEqualTo(1).WithMessage("Address number cannot be less than 1").When(x => x.AddressNumber.Value != 0);


            RuleFor(x => x.WorkFrom.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23").When(x => x.WorkFrom.Value != 0);

            RuleFor(x => x.WorkTo.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23").When(x => x.WorkTo.Value != 0);
        }
    }
}
