using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;

namespace RestaurantOrderAPI.Application.Validators
{
    public class StringValidation : AbstractValidator<string>
    {
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
