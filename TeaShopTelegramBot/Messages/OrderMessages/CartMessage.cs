using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class CartMessage
{
    private readonly OrderStringBuilder _stringBuilder = new();

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "💳 Способ оплаты", callbackData: "UPaymentMethod"),
            InlineKeyboardButton.WithCallbackData(text: "🛸 Способ доставки", callbackData: "VDeliveryMethod"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "❌ Удалить товар", callbackData: "ZRemoveProduct"),
            InlineKeyboardButton.WithCallbackData(text: "🤝 Подтвердить", callbackData: "XOrderConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "*Menu"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "💳 Payment method", callbackData: "UPaymentMethod"),
            InlineKeyboardButton.WithCallbackData(text: "🛸 Delivery method", callbackData: "VDeliveryMethod"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "❌ Remove product", callbackData: "ZRemoveProduct"),
            InlineKeyboardButton.WithCallbackData(text: "🤝 Order confirm", callbackData: "XOrderConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Menu ✨", callbackData: "*Menu"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "💳 Payment method", callbackData: "UPaymentMethod"),
            InlineKeyboardButton.WithCallbackData(text: "🛸 Delivery method", callbackData: "VDeliveryMethod"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "❌ Remove product", callbackData: "ZRemoveProduct"),
            InlineKeyboardButton.WithCallbackData(text: "🤝 Order confirm", callbackData: "XOrderConfirm"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Menu ✨", callbackData: "*Menu"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public CartMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, OrderDto orderDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        string text = _stringBuilder.GetStringForOrder(orderDto, language);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            text, _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            text, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            text, _hebrevInlineKeyboardMarkup);
    }

    public async Task GetMessage(long chatId, ITelegramBotClient client, OrderDto? orderDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (orderDto != null)
        {
            string text = _stringBuilder.GetStringForOrder(orderDto, language);

            if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
                text, _russianInlineKeyboardMarkup);

            if (language == Language.English) await MessageService.SendMessage(chatId, client,
                text, _englishInlineKeyboardMarkup);

            if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
                text, _hebrevInlineKeyboardMarkup);
        }       
    }
}
