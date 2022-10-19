using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;

namespace Application.TlgUsers.Queries;

public class GetAdminsQuery : IGetAdminsQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetAdminsQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<TlgUser>> GetAdminsAsync()
    {
        return await _context.Users
            .Where(u => u.IsAdmin == true)
            .ToListAsync();
    }
}
