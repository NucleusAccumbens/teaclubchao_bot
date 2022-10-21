namespace TeaShopTelegramBot.Models;

public abstract class ProductDto
{
    private decimal? _price;
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price
    {
        get
        {
            if (Discount != null && Discount > 0)
            {
                return _price - ((_price / 100) * Discount);
            }

            else return _price;
        }
        set
        {
            _price = value;
        }
    }

    public int? Discount { get; set; }
    public int? Count { get; set; }
    public string? PathToPhoto { get; set; }
    public bool InStock
    {
        get
        {
            if (Count > 0)
            {
                return true;
            }

            else return false;
        }
    }
}
