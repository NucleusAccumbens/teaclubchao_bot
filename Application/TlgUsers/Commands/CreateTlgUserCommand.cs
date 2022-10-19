using Application.Common.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Telegram.Bot.Types;

namespace Application.TlgUsers.Commands;

public class CreateTlgUserCommand : ICreateTlgUserCommand
{
    private readonly ITeaShopBotDbContext _context;

    public CreateTlgUserCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateTlgUserAsync(Chat chat)
    {
        var entity = new TlgUser()
        {
            ChatId = chat.Id,
            Username = chat.Username,
            Firstname = chat.FirstName,
            Lastname = chat.LastName,
            Language = Domain.Enums.Language.English,
            IsAdmin = false,
            IsKicked = false,
            IsDeleted = false
        };

        await _context.Users
            .AddAsync(entity);

        await _context.SaveChangesAsync();
    }
}

