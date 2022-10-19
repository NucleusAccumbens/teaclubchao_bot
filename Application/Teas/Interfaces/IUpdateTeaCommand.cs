using Domain.Entities;

namespace Application.Teas.Interfaces;

public interface IUpdateTeaCommand
{
    Task UpdateTeaDescriptionAsync(Tea tea);

    Task UpdateTeaCountAsync(Tea tea);

    Task UpdateTeaPriceAsync(Tea tea);

    Task UpdateTeaDiscountAsync(Tea tea);
}
