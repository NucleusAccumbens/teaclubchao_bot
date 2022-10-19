using Application.Common.Interfaces;
using Application.Herbs.Interfaces;

namespace Application.Herbs.Commands;

public class ChangeHerbCountCommand : IChangeHerbCountCommand
{
    private readonly ITeaShopBotDbContext _context;

    public ChangeHerbCountCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }


    public async Task AddOneToHerbCountAsync(long id)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null)
        {
            entity.Count += 1;

            await _context.SaveChangesAsync();
        }
    }

    public async Task SubtractOneFromHerbCountAsync(long id)
    {
        var entity = await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null && entity.Count > 0)
        {
            entity.Count -= 1;

            await _context.SaveChangesAsync();
        }
    }
}
