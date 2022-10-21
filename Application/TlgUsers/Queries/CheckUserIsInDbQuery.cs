using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Queries;

public class CheckUserIsInDbQuery : ICheckUserIsInDbQuery
{
    private readonly ITeaShopBotDbContext _context;

    public CheckUserIsInDbQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckUserIsInDbAsync(long chatId)
    {
        var entity = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity == null) return false;
        else return true;
    }
}
