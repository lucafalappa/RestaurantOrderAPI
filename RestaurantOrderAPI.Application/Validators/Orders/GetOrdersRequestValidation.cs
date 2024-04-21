using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Orders;

namespace RestaurantOrderAPI.Application.Validators.Orders
{
    public class GetOrdersRequestValidation :AbstractValidator<GetOrdersRequest>
    {
        // TODO : NOT NECESSARY BECAUSE OF JSON BODY NOT ALLOWED IN GET METHOD
        public GetOrdersRequestValidation()
        {
            RuleFor(request => request.StartDate)
                .NotNull()
                .WithMessage("StartDate field is required (null)")
                .NotEmpty()
                .WithMessage("StartDate field is required (empty)")
                .Must((request, startDate) => startDate < request.EndDate)
                .WithMessage("StartDate value must be lower than EndDate value");
            RuleFor(request => request.EndDate)
                .NotNull()
                .WithMessage("EndDate field is required (null)")
                .NotEmpty()
                .WithMessage("EndDate field is required (empty)")
                .Must((request, endDate) => endDate > request.StartDate)
                .WithMessage("EndDate value must be greater than StartDate value");
        }
    }
}
