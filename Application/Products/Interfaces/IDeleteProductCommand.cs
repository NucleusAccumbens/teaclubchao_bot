namespace Application.Products.Interfaces;

public interface IDeleteProductCommand
{
    Task DeleteProductAsync(long id);
}
