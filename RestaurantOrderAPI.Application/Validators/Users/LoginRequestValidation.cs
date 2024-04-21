using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Users;

namespace RestaurantOrderAPI.Application.Validators.Users
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .WithMessage("Email field is required (null)")
                .NotEmpty()
                .WithMessage("Email field is required (empty)")
                .IsCorrectEmail
                    (UserRequestsValidationMessages.EmailMessage);
            RuleFor(request => request.Password)
                .NotNull()
                .WithMessage("Password field is required (null)")
                .NotEmpty()
                .WithMessage("Password field is required (empty)")
                .IsCorrectPassword
                    (UserRequestsValidationMessages.PasswordMessage);
        }
    }
}
