using Application.Common.Interfaces;
using Application.Orders.Interfaces;
using Domain.Entities;

namespace Application.Orders.Commands;

public class CreateOrderCommand : ICreateOrderCommand
{
    private readonly ITeaShopBotDbContext _context;

    public CreateOrderCommand(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();

        return order;
    }
}
