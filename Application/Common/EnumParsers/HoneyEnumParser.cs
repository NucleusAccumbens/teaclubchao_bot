using Domain.Enums;

namespace Application.Common.EnumParsers;

public class HoneyEnumParser
{    
    public static string GetHoneyWeightStringValue(HoneyWeight? honeyWeight)
    {
        if (honeyWeight == HoneyWeight.ThreeHundredFifty) return "350";
        else return "950";
    }
}
