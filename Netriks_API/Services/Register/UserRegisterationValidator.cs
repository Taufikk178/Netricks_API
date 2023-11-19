using FluentValidation;
using Netricks_API.Entities.Register;

namespace Netricks_API.Services.Register
{
    public class UserRegisterationValidator : AbstractValidator<UserRegisteration>
    {
        public UserRegisterationValidator()
        {
            RuleFor(x => x.u_name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Minimum two characters required.");

            RuleFor(x => x.u_contact).NotEmpty()
                .WithMessage("Contact is required.")
                .Matches(@"^\d{10}$").WithMessage("Invalid phone number format. Use 10 digits.");

            RuleFor(x => x.u_email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Userame is required.")
                .MinimumLength(5).WithMessage("Minimum five characters required.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(10).WithMessage("Password Minimum ten characters required.");

            RuleFor(x => x.u_state)
                .NotEmpty().WithMessage("State is required.");
        }
    }
}