using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Validators.Messages;

namespace RestaurantOrderAPI.Application.Validators.Users
{
    /// <summary>
    /// Validator for <see cref="LoginRequest"/> objects
    /// </summary>
    public class LoginRequestValidation
        : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="LoginRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="LoginRequest"/> fields
        /// </summary>
        public LoginRequestValidation()
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
