using FluentValidation;

namespace RestaurantOrderAPI.Application.Validators
{
    public class DateTimeValidation : AbstractValidator<DateTime>
    {
        public DateTimeValidation()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("DateTime field is required (null)")
                .NotEmpty()
                .WithMessage("DateTime field is required (empty)")
                .Must(date => date != default(DateTime))
                .WithMessage("DateTime value is not valid");
        }
    }
}
