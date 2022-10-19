namespace Application.TlgUsers.Interfaces;

public interface IСheckUserIsAdminQuery
{
    Task<bool?> CheckUserIsAdminAsync(long chatId);
}

