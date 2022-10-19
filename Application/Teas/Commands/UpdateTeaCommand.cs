using Application.Common.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;

namespace Application.Teas.Commands;

public class UpdateTeaCommand : IUpdateTeaCommand
{
    private readonly ITeaShopBotDbContext _context;

    public UpdateTeaCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task UpdateTeaCountAsync(Tea tea)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == tea.Id);

        if (entity != null)
        {
            entity.Count = tea.Count;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTeaDescriptionAsync(Tea tea)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == tea.Id);

        if (entity != null)
        {
            entity.Description = tea.Description;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTeaDiscountAsync(Tea tea)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == tea.Id);

        if (entity != null)
        {
            entity.Discount = tea.Discount;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTeaPriceAsync(Tea tea)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == tea.Id);

        if (entity != null)
        {
            entity.Price = tea.Price;

            await _context.SaveChangesAsync();
        }
    }
}
