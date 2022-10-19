using Application.Common.Interfaces;
using Application.Herbs.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Herbs.Queries;

public class GetHerbsQuery : IGetHerbQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetHerbsQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Herb>> GetAltaiHerbsAsync()
    {
        return await _context.Herbs
            .Where(h => h.Region == HerbsRegion.Altai)
            .ToListAsync();
    }

    public async Task<List<Herb>> GetCaucasusHerbsAsync()
    {
        return await _context.Herbs
            .Where(h => h.Region == HerbsRegion.Caucasus)
            .ToListAsync();
    }

    public async Task<Herb?> GetHerbAsync(long id)
    {
        return await _context.Herbs
            .SingleOrDefaultAsync(h => h.Id == id);
    }

    public async Task<List<Herb>> GetKareliaHerbsAsync()
    {
        return await _context.Herbs
            .Where(h => h.Region == HerbsRegion.Karelia)
            .ToListAsync();
    }

    public async Task<List<Herb>> GetSiberiaHerbsAsync()
    {
        return await _context.Herbs
            .Where(h => h.Region == HerbsRegion.Siberia)
            .ToListAsync();
    }
}
