using Domain.Enums;

namespace Application.TlgUsers.Interfaces;

public interface IChangeLanguageCommand
{
    Task ChangeLanguageAsync(long chatId, Language language);
}
