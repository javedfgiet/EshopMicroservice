

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketQueryResult>;
    public record GetBasketQueryResult(ShoppingCart Cart);

    public class GetBasketHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
    {
        public async Task<GetBasketQueryResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {

            var basket = await _repository.GetBasket(request.UserName);

            return new GetBasketQueryResult(basket);
        }
    }
}
