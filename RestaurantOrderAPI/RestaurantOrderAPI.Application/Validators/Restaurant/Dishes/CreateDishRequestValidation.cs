using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Dishes;

namespace RestaurantOrderAPI.Application.Validators.Restaurant.Dishes
{
    /// <summary>
    /// Validator for <see cref="CreateDishRequest"/> objects
    /// </summary>
    public class CreateDishRequestValidation
        : AbstractValidator<CreateDishRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CreateDishRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="CreateDishRequest"/> fields
        /// </summary>
        public CreateDishRequestValidation()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .WithMessage("Name field is required (null)")
                .NotEmpty()
                .WithMessage("Name field is required (empty)")
                .IsOnlyLetters("Name field does not contain only letters");
            RuleFor(request => request.Price)
                .NotNull()
                .WithMessage("Price field is required (null)")
                .Must(value => decimal.TryParse(value.ToString(), out _))
                .WithMessage("Price value must be a decimal number")
                .GreaterThan(0)
                .WithMessage("Price value must be greater than 0");
            RuleFor(request => (int)(request.Type))
                .NotNull()
                .WithMessage("Type field is required (null)")
                .Must(value => int.TryParse(value.ToString(), out _))
                .WithMessage("Type value must be an int number")
                .IsDishTypeValid("Type value is not valid");
        }
    }
}
