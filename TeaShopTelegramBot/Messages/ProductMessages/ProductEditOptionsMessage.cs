using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.ProductMessages;

public class ProductEditOptionsMessage
{
    private readonly string _messageText = "Выбери, какой параметр хочешь отредактировать.";


    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, long productId, char callbackDataCode)
    {

        await MessageService.EditMessage(chatId, messageId, client, 
            _messageText, GetInlineKeyboardMarkup(productId, callbackDataCode));
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long? productId, char callbackDataCode)
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🪧 Описание", callbackData: $"{callbackDataCode}Description{productId}"),
                InlineKeyboardButton.WithCallbackData(text: "💰 Цена", callbackData: $"{callbackDataCode}Price{productId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "♾ Количество", callbackData: $"{callbackDataCode}Count{productId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: $"{callbackDataCode}Back{productId}"),
            },
        });
    }
}
