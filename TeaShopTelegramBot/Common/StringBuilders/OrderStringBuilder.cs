using Domain.Entities;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.StringBuilders;

public class OrderStringBuilder
{
    public string GetStringForOrder(OrderDto orderDto, Language? language)
    {
        if (language == Language.Russian) return GetStringInRussian(orderDto);

        if (language == Language.English) return GetStringInEnglish(orderDto);

        if (language == Language.Hebrew) return GetStringInHebrew(orderDto);

        return string.Empty; ;
    }

    private string GetStringInRussian(OrderDto orderDto)
    {
        string orderText = String.Empty;

        orderText += GetStringForProducts(orderDto.Products);

        orderText += $"<b>💰 Общая стоимость</b>: {orderDto.TotalProductPrice}\n" +
            $"<b>🛸 Способ доставки</b>: {OrderEnumParser.GetReceiptMethodStringValueInRussian(orderDto.ReceiptMethod)}\n" +
            $"<b>💳 Способ оплаты</b>: {OrderEnumParser.GetPaymentMethodStringValueInRussian(orderDto.PaymentMethod)}";

        if (orderDto.ReceiptMethod == ReceiptMethods.Boxberry && orderDto.Contacts != null ||
            orderDto.ReceiptMethod == ReceiptMethods.CDEK && orderDto.Contacts != null)
        {
            orderText += $"\n\n<b>Имя получателя:</b> {orderDto.Contacts.Name}\n" +
                $"<b>Номер телефона:</b> {orderDto.Contacts.Number}\n" +
                $"<b>Адрес пункта выдачи:</b> {orderDto.Contacts.Address}";
        }

        return orderText;
    }

    private string GetStringInEnglish(OrderDto orderDto)
    {
        string orderText = String.Empty;

        orderText += GetStringForProducts(orderDto.Products);

        orderText += $"<b>💰 Total price</b>: {orderDto.TotalProductPrice}\n" +
            $"<b>🛸 Delivery method</b>: {OrderEnumParser.GetReceiptMethodStringValueInEnglish(orderDto.ReceiptMethod)}\n" +
            $"<b>💳 Payment method</b>: {OrderEnumParser.GetPaymentMethodStringValueInEnglish(orderDto.PaymentMethod)}";

        if (orderDto.ReceiptMethod == ReceiptMethods.Boxberry && orderDto.Contacts != null ||
            orderDto.ReceiptMethod == ReceiptMethods.CDEK && orderDto.Contacts != null)
        {
            orderText += $"\n\n<b>Receiver name:</b> {orderDto.Contacts.Name}\n" +
                $"<b>Phone number:</b> {orderDto.Contacts.Number}\n" +
                $"<b>Pickup point address:</b> {orderDto.Contacts.Address}";
        }

        return orderText;
    }

    private string GetStringInHebrew(OrderDto orderDto)
    {
        string orderText = String.Empty;

        orderText += GetStringForProducts(orderDto.Products);

        orderText += $"<b>💰 Total price</b>: {orderDto.TotalProductPrice}\n" +
            $"<b>🛸 Delivery method</b>: {OrderEnumParser.GetReceiptMethodStringValueInEnglish(orderDto.ReceiptMethod)}\n" +
            $"<b>💳 Payment method</b>: {OrderEnumParser.GetPaymentMethodStringValueInEnglish(orderDto.PaymentMethod)}";

        if (orderDto.ReceiptMethod == ReceiptMethods.Boxberry && orderDto.Contacts != null ||
            orderDto.ReceiptMethod == ReceiptMethods.CDEK && orderDto.Contacts != null)
        {
            orderText += $"\n\n<b>Receiver name:</b> {orderDto.Contacts.Name}\n" +
                $"<b>Phone number:</b> {orderDto.Contacts.Number}\n" +
                $"<b>Pickup point address:</b> {orderDto.Contacts.Address}";
        }

        return orderText;
    }

    private string GetStringForProducts(List<ProductDto> products)
    {
        string prodText = string.Empty;

        foreach (var product in products)
        {
            if (product is TeaDto) prodText += $"🍃 <b>{product.Name}</b>\n" +
                    $"⚖️ {TeaEnumParser.GetTeaWeightStringValue((product as TeaDto).TeaWeight)} g\n";

            if (product is HerbDto) prodText += $"🌱 <b>{product.Name}</b>\n" +
                    $"⚖️ {HerbEnumParser.GetHerbWeightStringValue((product as HerbDto).Weight)} g\n";

            if (product is HoneyDto) prodText += $"🍯 <b>{product.Name}</b>\n" +
                    $"⚖️ {HoneyEnumParser.GetHoneyWeightStringValue((product as HoneyDto).HoneyWeight)} g\n";

            prodText += $"💰 {product.Price}\n\n";
        }

        return prodText;
    }
}
