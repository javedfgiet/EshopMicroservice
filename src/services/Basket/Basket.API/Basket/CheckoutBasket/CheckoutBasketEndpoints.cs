﻿
using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
    public record CheckOutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckOutBasketResponse>();

                return Results.Ok(response);
            })
         .WithName("CheckoutBasket")
         .Produces<CheckOutBasketResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Checkout Basket")
         .WithDescription("Checkout Basket");
        }
    }
}