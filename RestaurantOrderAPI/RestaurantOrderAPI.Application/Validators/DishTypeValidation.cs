using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Validators
{
    /// <summary>
    /// Validator for <see cref="DishType"/> fields
    /// </summary>
    public class DishTypeValidation : AbstractValidator<DishType>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DishTypeValidation"/> class
        /// Sets up validation rules for <see cref="DishType"/> fields
        /// </summary>
        public DishTypeValidation()
        {
            RuleFor(x => (int)(x))
                .NotNull()
                .WithMessage("Type field is required (null)")
                .Must(value => int.TryParse(value.ToString(), out _))
                .WithMessage("Type value must be an int number")
                .IsDishTypeValid("Type value is not valid");
        }
    }
}
