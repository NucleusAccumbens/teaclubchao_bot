using System.Security.Cryptography.X509Certificates;

namespace TeaShopTelegramBot.Models;

public class OrderDto
{
    public long? Id { get; set; }
    public long UserChatId { get; set; }
    public bool OrderStatus { get; set; }
    public string? Comment { get; set; }
    public PaymentMethods PaymentMethod { get; set; }
    public ReceiptMethods ReceiptMethod { get; set; }
    public decimal? TotalProductPrice
    {
        get
        {
            if (Products.Count > 0)
            {
                decimal? sum = 0;

                foreach (var product in Products)
                {
                    sum += product.Price;
                }

                return sum;
            }

            return 0;
        }
    }
    public ContactsDto? Contacts { get; set; } = new ContactsDto();

    public List<ProductDto>? Products { get; set; } = new List<ProductDto>();
}
