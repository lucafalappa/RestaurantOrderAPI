using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Users;

namespace RestaurantOrderAPI.Application.Validators.Users
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .WithMessage("Email field is required (null)")
                .NotEmpty()
                .WithMessage("Email field is required (empty)")
                .IsCorrectEmail
                    (UserRequestsValidationMessages.EmailMessage);
            RuleFor(request => request.Name)
                .NotNull()
                .WithMessage("Name field is required (null)")
                .NotEmpty()
                .WithMessage("Name field is required (empty)")
                .IsOnlyLetters("Name field does not contain only letters");
            RuleFor(request => request.Surname)
                .NotNull()
                .WithMessage("Surname field is required (null)")
                .NotEmpty()
                .WithMessage("Surname field is required (empty)")
                .IsOnlyLetters("Surname field does not contain only letters");
            RuleFor(request => request.Password)
                .NotNull()
                .WithMessage("Password field is required (null)")
                .NotEmpty()
                .WithMessage("Password field is required (empty)")
                .IsCorrectPassword
                    (UserRequestsValidationMessages.PasswordMessage);
            RuleFor(request => request.UserRole)
                .NotNull()
                .WithMessage("UserRole field is required (null)")
                .NotEmpty()
                .WithMessage("UserRole field is required (empty)")
                .IsCorrectRole("UserRole not admissible");
        }
    }
}
