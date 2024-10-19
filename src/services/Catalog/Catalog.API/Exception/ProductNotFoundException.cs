namespace Basket.API.Exception
{
    using BuildingBlocks.Exceptions;
    using System;
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(Guid Id):base("product",Id)
        {
            
        }
    }
}
