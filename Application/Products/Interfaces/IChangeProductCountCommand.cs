namespace Application.Products.Interfaces;

public interface IChangeProductCountCommand
{
    Task SubtractOneFromCountAsync(long? productId);
}
