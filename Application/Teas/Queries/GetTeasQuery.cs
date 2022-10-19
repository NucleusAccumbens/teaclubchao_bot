using Application.Common.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Teas.Queries;

public class GetTeasQuery : IGetTeaQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetTeasQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tea>> GetAllCraftTeasAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.CraftTeas)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllGreenTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.Green)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllOloongTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.Oolong)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllRedTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.Red)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllShenPuerTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.ShenPuer)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllShuPuerTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.ShuPuer)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllWhiteTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaTypes.White)
            .ToListAsync();
    }

    public async Task<Tea?> GetTeaAsync(long id)
    {
        return await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == id);
    }
}
