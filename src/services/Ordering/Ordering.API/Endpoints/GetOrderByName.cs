using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrderByNameRequest(string Name);
    public record GetOrderByNameResult(IEnumerator<OrderDto> Orders);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/{orderName}", async (string OrderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(OrderName));

                var response = result.Adapt<GetOrdersByNameResult>();

                return Results.Ok(response);
            }).WithName("Get Order By Name")
            .Produces<GetOrdersByNameResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Order By Name")
            .WithDescription("Get Order By Name");
        }
    }
}
