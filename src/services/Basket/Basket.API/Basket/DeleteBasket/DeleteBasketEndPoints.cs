
namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var resposne = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(resposne);
            })
                .WithName("Delete Basket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }
    }
}
