using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class DeliveryMethodMessage
{
    private readonly string _russianMessageText = $"Выбери способ доставки.";

    private readonly string _englishMessageText = $"Choose a delivery method.";

    private readonly string _hebrewMessageText = $"בחר שיטת משלוח.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {            
            InlineKeyboardButton.WithCallbackData(text: "🚛 СДЭК", callbackData: "VSDEK"),
            InlineKeyboardButton.WithCallbackData(text: "🚚 Boxberry", callbackData: "VBoxberry"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🚶🏾 Самовывоз", callbackData: "VPickup"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🚛 SDEK", callbackData: "VSDEK"),
            InlineKeyboardButton.WithCallbackData(text: "🚚 Boxberry", callbackData: "VBoxberry"),
        },
        new[]
        {            
            InlineKeyboardButton.WithCallbackData(text: "🚶🏾 Pickup", callbackData: "VPickup"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🚛 SDEK", callbackData: "VSDEK"),
            InlineKeyboardButton.WithCallbackData(text: "🚚 Boxberry", callbackData: "VBoxberry"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🚶🏾 Pickup", callbackData: "VPickup"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public DeliveryMethodMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            _russianMessageText, _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            _englishMessageText, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            _hebrewMessageText, _hebrevInlineKeyboardMarkup);
    }
}
