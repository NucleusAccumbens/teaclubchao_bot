using Application.Common.Interfaces;
using Application.Honeys.Interfaces;
using Domain.Entities;

namespace Application.Honeys.Commands;

public class UpdateHoneyCommand : IUpdateHoneyCommand
{
    private readonly ITeaShopBotDbContext _context;

    public UpdateHoneyCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task UpdateHoneyCountAsync(Honey honey)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(t => t.Id == honey.Id);

        if (entity != null)
        {
            entity.Count = honey.Count;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHoneyDescriptionAsync(Honey honey)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(t => t.Id == honey.Id);

        if (entity != null)
        {
            entity.Description = honey.Description;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHoneyDiscountAsync(Honey honey)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(t => t.Id == honey.Id);

        if (entity != null)
        {
            entity.Discount = honey.Discount;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHoneyPriceAsync(Honey honey)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(t => t.Id == honey.Id);

        if (entity != null)
        {
            entity.Price = honey.Price;

            await _context.SaveChangesAsync();
        }
    }
}
