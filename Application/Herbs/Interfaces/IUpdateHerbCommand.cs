using Domain.Entities;

namespace Application.Herbs.Interfaces;

public interface IUpdateHerbCommand
{
    Task UpdateHerbDescriptionAsync(Herb herb);

    Task UpdateHerbCountAsync(Herb herb);

    Task UpdateHerbPriceAsync(Herb herb);

    Task UpdateHerbDiscountAsync(Herb herb);
}
