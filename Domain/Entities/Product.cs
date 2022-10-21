namespace Domain.Entities;

public abstract class Product : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Discount { get; set; } = 0;
    public int? Count { get; set; }
    public string? PathToPhoto { get; set; }
    public bool InStock { get; set; }

    public List<Order>? Orders { get; private set; } = new List<Order>();
}

