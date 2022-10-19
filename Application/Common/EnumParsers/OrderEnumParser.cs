using Domain.Enums;

namespace Application.Common.EnumParsers;

public class OrderEnumParser
{
    public static string GetPaymentMethodStringValueInRussian(PaymentMethods method)
    {
        if (method == PaymentMethods.Cash) return "Наличными";
        if (method == PaymentMethods.Remittance) return "Переводом";
        else return "По ссылке";
    }

    public static string GetPaymentMethodStringValueInEnglish(PaymentMethods method)
    {
        if (method == PaymentMethods.Cash) return "Cash";
        if (method == PaymentMethods.Remittance) return "Remittance";
        else return "By link";
    }

    public static string GetPaymentMethodStringValueInHebrew(PaymentMethods method)
    {
        if (method == PaymentMethods.Cash) return "Cash";
        if (method == PaymentMethods.Remittance) return "Remittance";
        else return "By link";
    }

    public static string GetReceiptMethodStringValueInRussian(ReceiptMethods method)
    {
        if (method == ReceiptMethods.Pickup) return "Самовывоз";
        if (method == ReceiptMethods.Boxberry) return "Boxberry";
        else return "СДЭК";
    }

    public static string GetReceiptMethodStringValueInEnglish(ReceiptMethods method)
    {
        if (method == ReceiptMethods.Pickup) return "Pickup";
        if (method == ReceiptMethods.Boxberry) return "Boxberry";
        else return "CDEK";
    }

    public static string GetReceiptMethodStringValueInHebrew(ReceiptMethods method)
    {
        if (method == ReceiptMethods.Pickup) return "Pickup";
        if (method == ReceiptMethods.Boxberry) return "Boxberry";
        else return "CDEK";
    }
}
