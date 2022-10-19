namespace Application.Herbs.Interfaces;

public interface IChangeHerbCountCommand
{
    Task SubtractOneFromHerbCountAsync(long id);

    Task AddOneToHerbCountAsync(long id);
}
