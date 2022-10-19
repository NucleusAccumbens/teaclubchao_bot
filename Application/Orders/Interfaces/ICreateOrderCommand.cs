using Domain.Entities;

namespace Application.Orders.Interfaces;

public interface ICreateOrderCommand
{
    Task<Order> CreateOrderAsync(Order order);
}
