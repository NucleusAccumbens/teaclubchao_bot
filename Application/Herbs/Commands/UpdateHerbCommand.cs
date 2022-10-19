using Application.Common.Interfaces;
using Application.Herbs.Interfaces;
using Domain.Entities;

namespace Application.Herbs.Commands;

public class UpdateHerbCommand : IUpdateHerbCommand
{
    private readonly ITeaShopBotDbContext _context;

    public UpdateHerbCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task UpdateHerbCountAsync(Herb herb)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == herb.Id);

        if (entity != null)
        {
            entity.Count = herb.Count;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHerbDescriptionAsync(Herb herb)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == herb.Id);

        if (entity != null)
        {
            entity.Description = herb.Description;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHerbDiscountAsync(Herb herb)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == herb.Id);

        if (entity != null)
        {
            entity.Discount = herb.Discount;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateHerbPriceAsync(Herb herb)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == herb.Id);

        if (entity != null)
        {
            entity.Price = herb.Price;

            await _context.SaveChangesAsync();
        }
    }
}
