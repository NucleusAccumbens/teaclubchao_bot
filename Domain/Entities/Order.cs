namespace Domain.Entities;

public class Order : BaseAuditableEntity
{
    public long UserChatId { get; set; }
    public bool OrderStatus { get; set; }
    public string? Comment { get; set; }
    public PaymentMethods PaymentMethod { get; set; }
    public ReceiptMethods ReceiptMethod { get; set; }

    public Contacts? Contacts { get; set; }
    public List<Product> Products { get; private set; } = new List<Product>();
}

