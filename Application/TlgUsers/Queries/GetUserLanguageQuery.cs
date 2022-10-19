using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Enums;

namespace Application.TlgUsers.Queries;

public class GetUserLanguageQuery : IGetUserLanguageQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetUserLanguageQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<Language?> GetUserLanguageAsync(long chatId)
    {
        var entity = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        return entity?.Language;
    }
}
