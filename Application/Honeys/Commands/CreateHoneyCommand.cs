using Application.Common.Interfaces;
using Application.Honeys.Interfaces;
using Domain.Entities;

namespace Application.Honeys.Commands;
public class CreateHoneyCommand : ICreateHoneyCommand
{
    private readonly ITeaShopBotDbContext _context;

    public CreateHoneyCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateHoneyAsync(Honey honey)
    {
        await _context.Honey
            .AddAsync(honey);
        await _context.SaveChangesAsync();
    }
}
