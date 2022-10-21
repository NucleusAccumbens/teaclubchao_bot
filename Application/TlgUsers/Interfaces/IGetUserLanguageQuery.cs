using Domain.Enums;

namespace Application.TlgUsers.Interfaces;

public interface IGetUserLanguageQuery
{
    Task<Language?> GetUserLanguageAsync(long chatId);
}
