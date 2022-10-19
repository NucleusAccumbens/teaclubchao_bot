using Domain.Entities;

namespace Application.Teas.Interfaces;
public interface ICreateTeaCommand
{
    Task CreateTeaAsync(Tea tea);
}
