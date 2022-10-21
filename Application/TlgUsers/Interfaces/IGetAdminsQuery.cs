using Domain.Entities;

namespace Application.TlgUsers.Interfaces;

public interface IGetAdminsQuery
{
    Task<List<TlgUser>> GetAdminsAsync();
}
