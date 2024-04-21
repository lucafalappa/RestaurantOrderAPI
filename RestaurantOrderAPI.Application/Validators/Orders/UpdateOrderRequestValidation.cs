using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Orders;

namespace RestaurantOrderAPI.Application.Validators.Orders
{
    public class UpdateOrderRequestValidation : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidation()
        {
            RuleFor(request => request.DeliveryAddress)
                .NotNull()
                .WithMessage("DeliveryAddress field is required (null)")
                .NotEmpty()
                .WithMessage("DeliveryAddress field is required (empty)")
                .IsOnlyLetters("DeliveryAddress field does not contain only letters");
        }
    }
}
