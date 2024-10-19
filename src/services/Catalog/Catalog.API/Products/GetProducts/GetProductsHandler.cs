
using Marten.Pagination;

namespace Basket.API.Products.GetProducts
{
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session) :
        IQueryHandler<GetProductQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {

            var products = await session.Query<Product>()
                .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
