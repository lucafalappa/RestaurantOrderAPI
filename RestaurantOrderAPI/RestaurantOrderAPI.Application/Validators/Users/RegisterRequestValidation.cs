using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Validators.Messages;

namespace RestaurantOrderAPI.Application.Validators.Users
{
    /// <summary>
    /// Validator for <see cref="RegisterRequest"/> objects
    /// </summary>
    public class RegisterRequestValidation
        : AbstractValidator<RegisterRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="RegisterRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="RegisterRequest"/> fields
        /// </summary>
        public RegisterRequestValidation()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .WithMessage("Email field is required (null)")
                .NotEmpty()
                .WithMessage("Email field is required (empty)")
                .IsCorrectEmail
                    (UserRequestsValidationMessage.EmailMessage);
            RuleFor(request => request.Password)
                .NotNull()
                .WithMessage("Password field is required (null)")
                .NotEmpty()
                .WithMessage("Password field is required (empty)")
                .IsCorrectPassword
                    (UserRequestsValidationMessage.PasswordMessage);
        }
    }
}
