using Domain.Enums;

namespace Application.Common.EnumParsers;

public class HerbEnumParser
{
    public static string GetHerbWeightStringValue(HerbsWeight? herbsWeight)
    {
        if (herbsWeight == HerbsWeight.Fifty) return "50";
        if (herbsWeight == HerbsWeight.OneHundred) return "100";
        if (herbsWeight == HerbsWeight.OneHundredFifty) return "150";
        if (herbsWeight == HerbsWeight.TwoHundred) return "200";
        else return "250";
    }

    public static string GetHerbRegionStringValueInRussian(HerbsRegion? herbsRegion)
    {
        if (herbsRegion == HerbsRegion.Karelia) return "Карелия";
        if (herbsRegion == HerbsRegion.Altai) return "Алтай";
        if (herbsRegion == HerbsRegion.Caucasus) return "Кавказ";
        else return "Сибирь";
    }

    public static string GetHerbRegionStringValueInEnglish(HerbsRegion? herbsRegion)
    {
        if (herbsRegion == HerbsRegion.Karelia) return "Karelia";
        if (herbsRegion == HerbsRegion.Altai) return "Altai";
        if (herbsRegion == HerbsRegion.Caucasus) return "Caucasus";
        else return "Siberia";
    }

    public static string GetHerbRegionStringValueInHebrew(HerbsRegion? herbsRegion)
    {
        if (herbsRegion == HerbsRegion.Karelia) return "Karelia";
        if (herbsRegion == HerbsRegion.Altai) return "Altai";
        if (herbsRegion == HerbsRegion.Caucasus) return "Caucasus";
        else return "Siberia";
    }
}
