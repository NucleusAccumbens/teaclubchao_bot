using Application.Common.Interfaces;
using Application.Honeys.Interfaces;
using Domain.Entities;

namespace Application.Honeys.Queries;

public class GetHoneyQuery : IGetHoneyQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetHoneyQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Honey>> GetAllHoneyAsync()
    {
        return await _context.Honey
            .ToListAsync();
    }

    public async Task<Honey?> GetHoneyAsync(long id)
    {
        return await _context.Honey
            .SingleOrDefaultAsync(h => h.Id == id);
    }
}
