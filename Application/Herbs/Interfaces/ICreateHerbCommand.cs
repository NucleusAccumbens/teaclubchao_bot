using Domain.Entities;

namespace Application.Herbs.Interfaces;
public interface ICreateHerbCommand
{
    Task CreateHerbAsync(Herb herb);
}
