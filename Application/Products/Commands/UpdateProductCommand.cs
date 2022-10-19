using Application.Common.Interfaces;
using Application.Products.Interfaces;
using Domain.Entities;

namespace Application.Products.Commands;

public class UpdateProductCommand : IUpdateProductCommand
{
    private readonly ITeaShopBotDbContext _context;

    public UpdateProductCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task UpdateProductDiscountAsync(Product product)
    {
        var entity = await _context.Products
            .SingleOrDefaultAsync(t => t.Id == product.Id);

        if (entity != null)
        {
            entity.Discount = product.Discount;

            await _context.SaveChangesAsync();
        }
    }
}
