using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;

namespace RestaurantOrderAPI.Application.Validators
{
    /// <summary>
    /// Validator for string fields
    /// </summary>
    public class StringValidation : AbstractValidator<string>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StringValidation"/> class
        /// Sets up validation rules for string fields
        /// </summary>
        public StringValidation()
        {
            RuleFor(s => s)
                .NotNull()
                .WithMessage
                    ("String field is required (null)")
                .Length(0, 100)
                .WithMessage
                    ("String length must be between 0 and 100 characters")
                .IsOnlyLetters
                    ("String field does not contain only letters");
        }
    }
}
