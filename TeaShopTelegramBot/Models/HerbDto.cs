using Domain.Enums;

namespace TeaShopTelegramBot.Models;
public class HerbDto : ProductDto
{   
    public HerbsRegion? Region { get; set; }
    public HerbsWeight? Weight { get; set; }
}
