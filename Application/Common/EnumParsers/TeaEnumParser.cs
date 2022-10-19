using Domain.Enums;

namespace Application.Common.EnumParsers;

public class TeaEnumParser
{    
    public static string GetTeaTypeStringValueInRussian(TeaTypes? teaType)
    {
        if (teaType == null) return "значение не указано";
        if (teaType == TeaTypes.Red) return "Красный";
        if (teaType == TeaTypes.Green) return "Зелёный";
        if (teaType == TeaTypes.White) return "Белый";
        if (teaType == TeaTypes.Oolong) return "Улун";
        if (teaType == TeaTypes.ShuPuer) return "Шу Пуэр";
        if (teaType == TeaTypes.ShenPuer) return "Шен Пуэр";
        else return "Авторский чай";
    }

    public static string GetTeaFormStringValueInRussian(TeaForms? teaForm)
    {
        if (teaForm == null) return "значение не указано";
        if (teaForm == TeaForms.Pressed) return "Пресованный";
        else return "Рассыпной";
    }

    public static string GetTeaWeightStringValue(TeaWeight? teaWeight)
    {
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }

    public static string GetTeaTypeStringValueInEnglish(TeaTypes? teaType)
    {
        if (teaType == null) return "no value specified";
        if (teaType == TeaTypes.Red) return "Red";
        if (teaType == TeaTypes.Green) return "Green";
        if (teaType == TeaTypes.White) return "White";
        if (teaType == TeaTypes.Oolong) return "Oolong";
        if (teaType == TeaTypes.ShuPuer) return "Shu puer";
        if (teaType == TeaTypes.ShenPuer) return "Shen puer";
        else return "Craft";
    }

    public static string GetTeaFormStringValueInEnglish(TeaForms? teaForm)
    {
        if (teaForm == null) return "no value specified";
        if (teaForm == TeaForms.Pressed) return "Pressed";
        else return "Loose";
    }

    public static string GetTeaWeightStringValueInEnglish(TeaWeight? teaWeight)
    {
        if (teaWeight == null) return "no value specified";
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }

    public static string GetTeaTypeStringValueInHebrew(TeaTypes? teaType)
    {
        if (teaType == null) return "no value specified";
        if (teaType == TeaTypes.Red) return "Red";
        if (teaType == TeaTypes.Green) return "Green";
        if (teaType == TeaTypes.White) return "White";
        if (teaType == TeaTypes.Oolong) return "Oolong";
        if (teaType == TeaTypes.ShuPuer) return "Shu puer";
        if (teaType == TeaTypes.ShenPuer) return "Shen puer";
        else return "Craft";
    }

    public static string GetTeaFormStringValueInHebrew(TeaForms? teaForm)
    {
        if (teaForm == null) return "no value specified";
        if (teaForm == TeaForms.Pressed) return "Pressed";
        else return "Loose";
    }

    public static string GetTeaWeightStringValueInHebrew(TeaWeight? teaWeight)
    {
        if (teaWeight == null) return "no value specified";
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }
}
