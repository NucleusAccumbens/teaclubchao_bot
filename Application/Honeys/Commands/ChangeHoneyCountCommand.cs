using Application.Common.Interfaces;
using Application.Honeys.Interfaces;

namespace Application.Honeys.Commands;

public class ChangeHoneyCountCommand : IChangeHoneyCountCommand
{
    private readonly ITeaShopBotDbContext _context;

    public ChangeHoneyCountCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task AddOneToHoneyCountAsync(long id)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null)
        {
            entity.Count += 1;

            await _context.SaveChangesAsync();
        }
    }

    public async Task SubtractOneFromHoneyCountAsync(long id)
    {
        var entity = await _context.Honey
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null)
        {
            entity.Count -= 1;

            await _context.SaveChangesAsync();
        }
    }
}
