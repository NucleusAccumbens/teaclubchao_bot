using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class OrderConfirmMessage
{
    private readonly OrderStringBuilder _stringBuilder = new();

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🤝 Подтвердить заказ", callbackData: "XConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 В корзину 🛒", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🤝 Confirm the order", callbackData: "XConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 Back to cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🤝 Confirm the order", callbackData: "XConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 Back to cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public OrderConfirmMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, OrderDto orderDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        string message = GetMessage(language);

        string orderDescript = _stringBuilder.GetStringForOrder(orderDto, language);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            message + orderDescript, _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            message + orderDescript, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            message + orderDescript, _hebrevInlineKeyboardMarkup);
    }

    private string GetMessage(Language? language)
    {
        if (language == Language.Russian) return
                $"Проверьте детали заказа. Чтобы дополнить или изменить заказ, " +
                $"нажмите кнопку <b>\"В корзину\"</b>\n\n" +
                $"Чтобы информация о заказе была передана администратору, " +
                $"нажмите <b>\"Подтвердить заказ\"</b>\n\n";

        if (language == Language.English) return
                $"Check order details. To supplement or change an order, " +
                 $"click button<b> \"Back to cart\"</b>\n\n" +
                 $"To send the order information to the administrator, " +
                 $"click <b>\"Confirm the order\"</b>\n\n";

        return $"בדוק את פרטי ההזמנה. כדי להוסיף או לשנות הזמנה, " +
                 $" לחץ על כפתור <b>\"חזור לעגלה\"</b>\n\n" +
                 $"כדי לשלוח את פרטי ההזמנה למנהל המערכת, " +
                 $"לחץ על <b>\"אשר הזמנה\"</b>\n\n";
    }
}
