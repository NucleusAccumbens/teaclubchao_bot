using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.StringBuilders;

public class ProductStringBuilder
{
    public string GetStringForProducts(ProductDto product)
    {
        string prodText = string.Empty;

        if (product is TeaDto) prodText += $"🍃 <b>{product.Name}</b>\n" +
                    $"⚖️ {TeaEnumParser.GetTeaWeightStringValue((product as TeaDto).TeaWeight)} g\n";

        if (product is HerbDto) prodText += $"🌱 <b>{product.Name}</b>\n" +
                $"⚖️ {HerbEnumParser.GetHerbWeightStringValue((product as HerbDto).Weight)} g\n";

        if (product is HoneyDto) prodText += $"🍯 <b>{product.Name}</b>\n" +
                $"⚖️ {HoneyEnumParser.GetHoneyWeightStringValue((product as HoneyDto).HoneyWeight)} g\n";

        prodText += $"💰 {product.Price}\n\n";

        return prodText;
    }
}
