using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Validators
{
    public class DishTypeValidation : AbstractValidator<DishType>
    {
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
