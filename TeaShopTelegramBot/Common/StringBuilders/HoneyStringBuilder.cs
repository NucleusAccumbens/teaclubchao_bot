using System.Security.Cryptography.X509Certificates;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.StringBuilders;

public class HoneyStringBuilder
{
    public static string GetStringForHoney(HoneyDto honeyDto, Language? language)
    {
        if (language == Language.Russian) return GetStringInRussian(honeyDto);

        if (language == Language.English) return GetStringInEnglish(honeyDto);

        if (language == Language.Hebrew) return GetStringInHebrew(honeyDto);

        return string.Empty; ;
    }

    private static string GetStringInRussian(HoneyDto honeyDto)
    {
        string honeyText = String.Empty;

        if (honeyDto != null)
        {
            if (honeyDto.Discount != null && honeyDto.Discount > 0) honeyText += $"🎁 <b>Скидка {honeyDto.Discount}% </b>🎁\n\n";

            if (honeyDto.Name != null) honeyText += $"<b>Название:</b> {honeyDto.Name}\n";

            if (honeyDto.Description != null) honeyText += $"<b>Описание:</b> {honeyDto.Description}\n";

            if (honeyDto.HoneyWeight != null) honeyText += $"<b>Вес:</b> " +
                    $"{HoneyEnumParser.GetHoneyWeightStringValue(honeyDto.HoneyWeight)}\n";

            if (honeyDto.Price != null && honeyDto.Discount != null && honeyDto.Discount > 0)
            {              
                honeyText += $"\n<b>Старая цена:</b> {honeyDto.Price + (honeyDto.Price / (100 - honeyDto.Discount) * honeyDto.Discount)}\n" +
                    $"<b>Новая цена:</b> {honeyDto.Price}\n\n";
            }

            if (honeyDto.Price != null && honeyDto.Discount == null || honeyDto.Price != null && honeyDto.Discount == 0) 
                honeyText += $"<b>Цена:</b> {honeyDto.Price}\n\n";

            if (honeyDto.Count != null) honeyText += $"<b>В наличии:</b> {honeyDto.Count}";
        }

        return honeyText;
    }

    private static string GetStringInEnglish(HoneyDto honeyDto)
    {
        string honeyText = String.Empty;

        if (honeyDto != null)
        {
            if (honeyDto.Discount != null && honeyDto.Discount > 0) honeyText += $"🎁 <b>Discount {honeyDto.Discount}% </b>🎁\n\n";

            if (honeyDto.Name != null) honeyText += $"<b>Name:</b> {honeyDto.Name}\n";

            if (honeyDto.Description != null) honeyText += $"<b>Description:</b> {honeyDto.Description}\n";

            if (honeyDto.HoneyWeight != null) honeyText += $"<b>Weight:</b> " +
                    $"{HoneyEnumParser.GetHoneyWeightStringValue(honeyDto.HoneyWeight)}\n";

            if (honeyDto.Price != null && honeyDto.Discount != null && honeyDto.Discount > 0)
            {
                honeyText += $"\n<b>Old price:</b> {honeyDto.Price + (honeyDto.Price / (100 - honeyDto.Discount) * honeyDto.Discount)}\n" +
                    $"<b>New price:</b> {honeyDto.Price}\n\n";
            }

            if (honeyDto.Price != null && honeyDto.Discount == null || honeyDto.Price != null && honeyDto.Discount == 0)
                honeyText += $"<b>Price:</b> {honeyDto.Price}\n\n";

            if (honeyDto.Count != null) honeyText += $"<b>Available:</b> {honeyDto.Count}";
        }

        return honeyText;
    }

    private static string GetStringInHebrew(HoneyDto honeyDto)
    {
        string honeyText = String.Empty;

        if (honeyDto != null)
        {
            if (honeyDto.Discount != null && honeyDto.Discount > 0) honeyText += $"🎁 <b>Discount {honeyDto.Discount}% </b>🎁\n\n";

            if (honeyDto.Name != null) honeyText += $"<b>Name:</b> {honeyDto.Name}\n";

            if (honeyDto.Description != null) honeyText += $"<b>Description:</b> {honeyDto.Description}\n";

            if (honeyDto.HoneyWeight != null) honeyText += $"<b>Weight:</b> " +
                    $"{HoneyEnumParser.GetHoneyWeightStringValue(honeyDto.HoneyWeight)}\n";

            if (honeyDto.Price != null && honeyDto.Discount != null && honeyDto.Discount > 0)
            {
                honeyText += $"\n<b>Old price:</b> {honeyDto.Price + (honeyDto.Price / (100 - honeyDto.Discount) * honeyDto.Discount)}\n" +
                    $"<b>New price:</b> {honeyDto.Price}\n\n";
            }

            if (honeyDto.Price != null && honeyDto.Discount == null || honeyDto.Price != null && honeyDto.Discount == 0)
                honeyText += $"<b>Price:</b> {honeyDto.Price}\n\n";

            if (honeyDto.Count != null) honeyText += $"<b>Available:</b> {honeyDto.Count}";
        }

        return honeyText;
    }
}
