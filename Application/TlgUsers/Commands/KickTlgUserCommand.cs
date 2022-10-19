using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;

namespace Application.TlgUsers.Commands;

internal class KickTlgUserCommand : IKickTlgUserCommand
{
    private readonly ITeaShopBotDbContext _context;

    public KickTlgUserCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckTlgUserIsKicked(long? chatId)
    {
        if (chatId == null) return true;

        var tlgUser = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (tlgUser != null)
        {
            return tlgUser.IsKicked;
        }

        return false;
    }

    public async Task ManageTlgUserKickingAsync(long chatId)
    {
        var tlgUser = await _context.Users
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (tlgUser != null)
        {
            if (tlgUser.IsKicked == false)
            {
                await ChangeTlgUserIsKickedValues(tlgUser, true);

                return;
            }
            if (tlgUser.IsKicked == true)
            {
                await ChangeTlgUserIsKickedValues(tlgUser, false);
            }
        }
    }

    private async Task ChangeTlgUserIsKickedValues(TlgUser tlgUser, bool value)
    {
        tlgUser.IsKicked = value;

        await _context.SaveChangesAsync();
    }
}

