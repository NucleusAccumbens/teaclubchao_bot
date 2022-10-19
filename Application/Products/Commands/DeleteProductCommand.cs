using Application.Common.Interfaces;
using Application.Products.Interfaces;
using Domain.Entities;

namespace Application.Products.Commands;

public class DeleteProductCommand : IDeleteProductCommand
{
    private readonly ITeaShopBotDbContext _context;

    public DeleteProductCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task DeleteProductAsync(long id)
    {
        var product = await _context.Products
            .SingleOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            _context.Products
                .Remove(product);
            
            await _context.SaveChangesAsync();
        }
    }
}
