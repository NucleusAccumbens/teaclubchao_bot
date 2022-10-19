using Application.Common.Interfaces;
using Application.Herbs.Interfaces;
using Domain.Entities;

namespace Application.Herbs.Commands;

public class CreateHerbCommand : ICreateHerbCommand
{
    private readonly ITeaShopBotDbContext _context;

    public CreateHerbCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateHerbAsync(Herb herb)
    {
        await _context.Herbs
            .AddAsync(herb);
        await _context.SaveChangesAsync();
    }
}
