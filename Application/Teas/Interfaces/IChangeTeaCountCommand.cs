namespace Application.Teas.Interfaces;

public interface IChangeTeaCountCommand
{
    Task AddOneToTeaCountAsync(long id);

    Task SubtractOneFromTeaCountAsync(long id);
}
