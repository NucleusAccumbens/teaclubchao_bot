using Domain.Entities;

namespace Application.Products.Interfaces;

public interface IGetProductsQuery
{
    Task<List<Product>?> GetDiscountProductsAsync();

    Task<Product?> GetProductAsync(long id);
}
