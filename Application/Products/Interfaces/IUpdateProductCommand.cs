using Domain.Entities;

namespace Application.Products.Interfaces;

public interface IUpdateProductCommand
{
    Task UpdateProductDiscountAsync(Product product);
}
