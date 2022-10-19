using Domain.Entities;

namespace Application.Honeys.Interfaces;

public interface IGetHoneyQuery
{
    Task<List<Honey>> GetAllHoneyAsync();

    Task<Honey?> GetHoneyAsync(long id);
}
