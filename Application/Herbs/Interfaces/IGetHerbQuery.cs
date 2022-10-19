using Domain.Entities;

namespace Application.Herbs.Interfaces;

public interface IGetHerbQuery
{
    Task<List<Herb>> GetAltaiHerbsAsync();

    Task<List<Herb>> GetKareliaHerbsAsync();

    Task<List<Herb>> GetCaucasusHerbsAsync();

    Task<List<Herb>> GetSiberiaHerbsAsync();

    Task<Herb?> GetHerbAsync(long id);
}
