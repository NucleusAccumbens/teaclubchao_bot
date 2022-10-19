using Application.Common.Interfaces;
using Application.Teas.Interfaces;

namespace Application.Teas.Commands;

public class ChangeTeaCountCommand : IChangeTeaCountCommand
{
    private readonly ITeaShopBotDbContext _context;

    public ChangeTeaCountCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task AddOneToTeaCountAsync(long id)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null)
        {
            entity.Count += 1;

            await _context.SaveChangesAsync();
        }
    }

    public async Task SubtractOneFromTeaCountAsync(long id)
    {
        var entity = await _context.Teas
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity != null)
        {
            entity.Count -= 1;

            await _context.SaveChangesAsync();
        }
    }
}
