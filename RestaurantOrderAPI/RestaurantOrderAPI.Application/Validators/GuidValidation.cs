using FluentValidation;

namespace RestaurantOrderAPI.Application.Validators
{
    /// <summary>
    /// Validator for Guid fields
    /// </summary>
    public class GuidValidation : AbstractValidator<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="GuidValidation"/> class
        /// Sets up validation rules for Guid fields
        /// </summary>
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
