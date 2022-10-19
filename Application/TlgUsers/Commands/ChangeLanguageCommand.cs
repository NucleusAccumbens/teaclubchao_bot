using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Enums;

namespace Application.TlgUsers.Commands;

public class ChangeLanguageCommand : IChangeLanguageCommand
{
    private readonly ITeaShopBotDbContext _context;

    public ChangeLanguageCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task ChangeLanguageAsync(long chatId, Language language)
    {
        var entity = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            entity.Language = language;

            await _context.SaveChangesAsync();
        }
    }
}
