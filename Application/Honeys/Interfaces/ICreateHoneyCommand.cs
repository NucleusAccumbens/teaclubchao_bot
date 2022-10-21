using Domain.Entities;

namespace Application.Honeys.Interfaces;

public interface ICreateHoneyCommand 
{
    Task CreateHoneyAsync(Honey honey);
}
