using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.StringBuilders;

public class TeaStringBuilder
{    
    public static string GetStringForTea(TeaDto teaDto, Language? language)
    {
        if (language == Language.Russian) return GetStringInRussian(teaDto);

        if (language == Language.English) return GetStringInEnglish(teaDto);

        if (language == Language.Hebrew) return GetStringInHebrew(teaDto);

        return string.Empty; ;
    }

    private static string GetStringInRussian(TeaDto teaDto)
    {
        string teaText = String.Empty;

        if (teaDto != null)
        {
            if (teaDto.Discount != null && teaDto.Discount > 0) teaText += $"🎁 <b>Скидка {teaDto.Discount}% </b>🎁\n\n";

            if (teaDto.TeaType != null) teaText += $"<b>Сорт:</b> " +
                    $"{TeaEnumParser.GetTeaTypeStringValueInRussian(teaDto.TeaType)}\n";

            if (teaDto.Name != null) teaText += $"<b>Название:</b> {teaDto.Name}\n";

            if (teaDto.Description != null) teaText += $"<b>Описание:</b> {teaDto.Description}\n";

            if (teaDto.TeaWeight != null) teaText += $"<b>Вес:</b> " +
                    $"{TeaEnumParser.GetTeaWeightStringValue(teaDto.TeaWeight)}\n";

            if (teaDto.TeaForm != null) teaText += $"<b>Форма:</b> " +
                    $"{TeaEnumParser.GetTeaFormStringValueInRussian(teaDto.TeaForm)}\n";

            if (teaDto.Price != null && teaDto.Discount != null && teaDto.Discount > 0)
            {
                teaText += $"\n<b>Старая цена:</b> {teaDto.Price + ((teaDto.Price / (100 - teaDto.Discount)) * teaDto.Discount)}\n" +
                    $"<b>Новая цена:</b> {teaDto.Price}\n\n";
            }

            if (teaDto.Price != null && teaDto.Discount == null || teaDto.Price != null && teaDto.Discount == 0) 
                teaText += $"<b>Цена:</b> {teaDto.Price}\n\n";

            if (teaDto.Count != null) teaText += $"<b>В наличии:</b> {teaDto.Count}";
        }

        return teaText;
    }

    private static string GetStringInEnglish(TeaDto teaDto)
    {
        string teaText = String.Empty;

        if (teaDto != null)
        {
            if (teaDto.Discount != null && teaDto.Discount > 0) teaText += $"🎁 <b>Discount {teaDto.Discount}% </b>🎁\n\n";

            if (teaDto.TeaType != null) teaText += $"<b>Variety:</b> " +
                    $"{TeaEnumParser.GetTeaTypeStringValueInHebrew(teaDto.TeaType)}\n";

            if (teaDto.Name != null) teaText += $"<b>Name:</b> {teaDto.Name}\n";

            if (teaDto.Description != null) teaText += $"<b>Description:</b> {teaDto.Description}\n";

            if (teaDto.TeaWeight != null) teaText += $"<b>Weight:</b> " +
                    $"{TeaEnumParser.GetTeaWeightStringValueInHebrew(teaDto.TeaWeight)}\n";

            if (teaDto.TeaForm != null) teaText += $"<b>Form:</b> " +
                    $"{TeaEnumParser.GetTeaFormStringValueInHebrew(teaDto.TeaForm)}\n";

            if (teaDto.Price != null && teaDto.Discount != null && teaDto.Discount > 0)
            {
                teaText += $"\n<b>Old price:</b> {teaDto.Price + ((teaDto.Price / (100 - teaDto.Discount)) * teaDto.Discount)}\n" +
                    $"<b>New price:</b> {teaDto.Price}\n\n";
            }

            if (teaDto.Price != null && teaDto.Discount == null || teaDto.Price != null && teaDto.Discount == 0)
                teaText += $"<b>Price:</b> {teaDto.Price}\n\n";

            if (teaDto.Count != null) teaText += teaText += $"<b>Available:</b> {teaDto.Count}";
        }

        return teaText;
    }

    private static string GetStringInHebrew(TeaDto teaDto)
    {
        string teaText = String.Empty;

        if (teaDto != null)
        {
            if (teaDto.Discount != null && teaDto.Discount > 0) teaText += $"🎁 <b>Discount {teaDto.Discount}% </b>🎁\n\n";

            if (teaDto.TeaType != null) teaText += $"<b>Variety:</b> " +
                    $"{TeaEnumParser.GetTeaTypeStringValueInHebrew(teaDto.TeaType)}\n";

            if (teaDto.Name != null) teaText += $"<b>Name:</b> {teaDto.Name}\n";

            if (teaDto.Description != null) teaText += $"<b>Description:</b> {teaDto.Description}\n";

            if (teaDto.TeaWeight != null) teaText += $"<b>Weight:</b> " +
                    $"{TeaEnumParser.GetTeaWeightStringValueInHebrew(teaDto.TeaWeight)}\n";

            if (teaDto.TeaForm != null) teaText += $"<b>Form:</b> " +
                    $"{TeaEnumParser.GetTeaFormStringValueInHebrew(teaDto.TeaForm)}\n";

            if (teaDto.Price != null && teaDto.Discount != null && teaDto.Discount > 0)
            {
                teaText += $"\n<b>Old price:</b> {teaDto.Price + ((teaDto.Price / (100 - teaDto.Discount)) * teaDto.Discount)}\n" +
                    $"<b>New price:</b> {teaDto.Price}\n\n";
            }

            if (teaDto.Price != null && teaDto.Discount == null || teaDto.Price != null && teaDto.Discount == 0)
                teaText += $"<b>Price:</b> {teaDto.Price}\n\n";

            if (teaDto.Count != null) teaText += teaText += $"<b>Available:</b> {teaDto.Count}";
        }

        return teaText;
    }
}
