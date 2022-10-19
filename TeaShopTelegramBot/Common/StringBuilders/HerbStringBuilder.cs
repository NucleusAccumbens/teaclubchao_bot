using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.StringBuilders;

public class HerbStringBuilder
{ 
    public static string GetStringForHerb(HerbDto herbDto, Language? language)
    {
        if (language == Language.Russian) return GetStringInRussian(herbDto);

        if (language == Language.English) return GetStringInEnglish(herbDto);

        if (language == Language.Hebrew) return GetStringInHebrew(herbDto);

        return string.Empty; ;
    }

    private static string GetStringInRussian(HerbDto herbDto)
    {
        string herbText = String.Empty;

        if (herbDto != null)
        {
            if (herbDto.Discount != null && herbDto.Discount > 0) herbText += $"🎁 <b>Скидка {herbDto.Discount}% </b>🎁\n\n";

            if (herbDto.Region != null) herbText += $"<b>Регион:</b> " +
                    $"{HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n";

            if (herbDto.Name != null) herbText += $"<b>Название:</b> {herbDto.Name}\n";

            if (herbDto.Description != null) herbText += $"<b>Описание:</b> {herbDto.Description}\n";

            if (herbDto.Weight != null) herbText += $"<b>Вес:</b> " +
                    $"{HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n";

            if (herbDto.Price != null && herbDto.Discount != null && herbDto.Discount > 0)
            {
                herbText += $"\n<b>Старая цена:</b> {herbDto.Price + ((herbDto.Price / (100 - herbDto.Discount)) * herbDto.Discount)}\n" +
                    $"<b>Новая цена:</b> {herbDto.Price}\n\n";
            }

            if (herbDto.Price != null && herbDto.Discount == 0 || herbDto.Price != null && herbDto.Discount == null) 
                herbText += $"<b>Цена:</b> {herbDto.Price}\n\n";

            if (herbDto.Count != null) herbText += $"<b>В наличии:</b> {herbDto.Count}";
        }

        return herbText;
    }

    private static string GetStringInEnglish(HerbDto herbDto)
    {
        string herbText = String.Empty;

        if (herbDto != null)
        {
            if (herbDto.Discount != null && herbDto.Discount > 0) herbText += $"🎁 <b>Discount {herbDto.Discount}% </b>🎁\n\n";

            if (herbDto.Region != null) herbText += $"<b>Region:</b> " +
                    $"{HerbEnumParser.GetHerbRegionStringValueInEnglish(herbDto.Region)}\n";

            if (herbDto.Name != null) herbText += $"<b>Name:</b> {herbDto.Name}\n";

            if (herbDto.Description != null) herbText += $"<b>Description:</b> {herbDto.Description}\n";

            if (herbDto.Weight != null) herbText += $"<b>Weight:</b> " +
                    $"{HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n";

            if (herbDto.Price != null && herbDto.Discount != null && herbDto.Discount > 0)
            {
                herbText += $"\n<b>Old price:</b> {herbDto.Price + ((herbDto.Price / (100 - herbDto.Discount)) * herbDto.Discount)}\n" +
                    $"<b>New price:</b> {herbDto.Price}\n\n";
            }

            if (herbDto.Price != null && herbDto.Discount == 0 || herbDto.Price != null && herbDto.Discount == null)
                herbText += $"<b>Price:</b> {herbDto.Price}\n\n";

            if (herbDto.Count != null) herbText += $"<b>Available:</b> {herbDto.Count}";
        }

        return herbText;
    }

    private static string GetStringInHebrew(HerbDto herbDto)
    {
        string herbText = String.Empty;

        if (herbDto != null)
        {
            if (herbDto.Discount != null && herbDto.Discount > 0) herbText += $"🎁 <b>Discount {herbDto.Discount}% </b>🎁\n\n";

            if (herbDto.Region != null) herbText += $"<b>Region:</b> " +
                    $"{HerbEnumParser.GetHerbRegionStringValueInEnglish(herbDto.Region)}\n";

            if (herbDto.Name != null) herbText += $"<b>Name:</b> {herbDto.Name}\n";

            if (herbDto.Description != null) herbText += $"<b>Description:</b> {herbDto.Description}\n";

            if (herbDto.Weight != null) herbText += $"<b>Weight:</b> " +
                    $"{HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n";

            if (herbDto.Price != null && herbDto.Discount != null && herbDto.Discount > 0)
            {
                herbText += $"\n<b>Old price:</b> {herbDto.Price + ((herbDto.Price / (100 - herbDto.Discount)) * herbDto.Discount)}\n" +
                    $"<b>New price:</b> {herbDto.Price}\n\n";
            }

            if (herbDto.Price != null && herbDto.Discount == 0 || herbDto.Price != null && herbDto.Discount == null)
                herbText += $"<b>Price:</b> {herbDto.Price}\n\n";

            if (herbDto.Count != null) herbText += $"<b>Available:</b> {herbDto.Count}";
        }

        return herbText;
    }
}
