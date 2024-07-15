using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Orders;

namespace RestaurantOrderAPI.Application.Validators.Restaurant.Orders
{
    /// <summary>
    /// Validator for <see cref="UpdateOrderRequest"/> objects
    /// </summary>
    public class UpdateOrderRequestValidation
        : AbstractValidator<UpdateOrderRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UpdateOrderRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="UpdateOrderRequest"/> fields
        /// </summary>
        public UpdateOrderRequestValidation()
        {
            RuleFor(request => request.DeliveryAddress)
                .NotNull()
                .WithMessage("DeliveryAddress field is required (null)")
                .NotEmpty()
                .WithMessage("DeliveryAddress field is required (empty)")
                .IsOnlyLetters("DeliveryAddress field does not " +
                    "contain only letters");
        }
    }
}
