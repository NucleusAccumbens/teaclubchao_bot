using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Queries;

internal class СheckUserIsAdminQuery : IСheckUserIsAdminQuery
{
    private readonly ITeaShopBotDbContext _context;

    public СheckUserIsAdminQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool?> CheckUserIsAdminAsync(long chatId)
    {
        var entity = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        return entity?.IsAdmin;
    }
}

