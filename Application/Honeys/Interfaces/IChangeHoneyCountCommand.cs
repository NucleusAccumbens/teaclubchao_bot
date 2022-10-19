namespace Application.Honeys.Interfaces;

public interface IChangeHoneyCountCommand
{
    Task SubtractOneFromHoneyCountAsync(long id);

    Task AddOneToHoneyCountAsync(long id);
}
