using Application.Common.Interfaces;
using Application.Products.Interfaces;

namespace Application.Products.Commands;

public class ChangeProductCountCommand : IChangeProductCountCommand
{
    private readonly ITeaShopBotDbContext _context;

    public ChangeProductCountCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task SubtractOneFromCountAsync(long? productId)
    {
        var product = await _context.Products
            .SingleOrDefaultAsync(p => p.Id == productId);

        if (product != null)
        {
            product.Count -= 1;

            await _context.SaveChangesAsync();
        }        
    }
}
