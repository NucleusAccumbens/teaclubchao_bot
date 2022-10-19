using Application.Common.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;

namespace Application.Teas.Commands;

public class CreateTeaCommand : ICreateTeaCommand
{
    private readonly ITeaShopBotDbContext _context; 

    public CreateTeaCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateTeaAsync(Tea tea)
    {
        await _context.Teas
            .AddAsync(tea);

        await _context.SaveChangesAsync();
    }
}
