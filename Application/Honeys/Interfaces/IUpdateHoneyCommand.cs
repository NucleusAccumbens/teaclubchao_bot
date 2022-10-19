using Domain.Entities;

namespace Application.Honeys.Interfaces;

public interface IUpdateHoneyCommand
{
    Task UpdateHoneyDescriptionAsync(Honey honey);

    Task UpdateHoneyCountAsync(Honey honey);

    Task UpdateHoneyPriceAsync(Honey honey);

    Task UpdateHoneyDiscountAsync(Honey honey);
}
