namespace Domain.Entities;

public class Contacts : BaseAuditableEntity
{
    public string? Name { get; set; } 
    public string? Number { get; set; }
    public string? Address { get; set; }

    public long? OrderId { get; set; }
    public Order? Order { get; set; }

}

