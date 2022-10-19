using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class PaymentMethodMessage
{
    private readonly string _russianMessageText = $"Выбери способ оплаты.";

    private readonly string _englishMessageText = $"Choose a payment method.";

    private readonly string _hebrewMessageText = $"בחר שיטת תשלום.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "💸 Переводом", callbackData: "URemittance"),
            InlineKeyboardButton.WithCallbackData(text: "💵 Наличными", callbackData: "UCash"),
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
            InlineKeyboardButton.WithCallbackData(text: "💸 Remittance", callbackData: "URemittance"),
            InlineKeyboardButton.WithCallbackData(text: "💵 Cash", callbackData: "UCash"),
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
            InlineKeyboardButton.WithCallbackData(text: "💸 Remittance", callbackData: "URemittance"),
            InlineKeyboardButton.WithCallbackData(text: "💵 Cash", callbackData: "UCash"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public PaymentMethodMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
