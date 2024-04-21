using FluentValidation;

namespace RestaurantOrderAPI.Application.Validators
{
    public class GuidValidation : AbstractValidator<Guid>
    {
        public GuidValidation()
        {
            RuleFor(guid => guid)
                .NotNull()
                .WithMessage("GUID field is required (null)")
                .NotEmpty()
                .WithMessage("GUID field is required (empty)")
                .Must(x => Guid.TryParse(x.ToString(), out _))
                .WithMessage("GUID value is not valid");
        }
    }
}
