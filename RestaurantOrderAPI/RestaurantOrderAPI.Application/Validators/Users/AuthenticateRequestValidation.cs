using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Validators.Messages;

namespace RestaurantOrderAPI.Application.Validators.Users
{
    /// <summary>
    /// Validator for <see cref="AuthenticateRequest"/> objects
    /// </summary>
    public class AuthenticateRequestValidation
        : AbstractValidator<AuthenticateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="AuthenticateRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="AuthenticateRequest"/> fields
        /// </summary>
        public AuthenticateRequestValidation()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .WithMessage("Email field is required (null)")
            .NotEmpty()
                .WithMessage("Email field is required (empty)")
                .IsCorrectEmail
                    (UserRequestsValidationMessage.EmailMessage);
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
                    (UserRequestsValidationMessage.PasswordMessage);
        }
    }
}
