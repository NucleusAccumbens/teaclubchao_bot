using Application.Common.Interfaces;
using Application.Products.Interfaces;
using Domain.Entities;

namespace Application.Products.Queries;

public class GetProductsQuery : IGetProductsQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetProductsQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>?> GetDiscountProductsAsync()
    {
        return await _context.Products
            .Where(p => p.Discount != null)
            .Where(p => p.Discount > 0)
            .ToListAsync();
    }

    public async Task<Product?> GetProductAsync(long id)
    {
        return await _context.Products
            .SingleOrDefaultAsync(p => p.Id == id);
    }
}
