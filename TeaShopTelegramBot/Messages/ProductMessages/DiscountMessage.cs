using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.ProductMessages;

public class DiscountMessage
{
    private readonly string _messageText = "Сколько процентов от цены будет составлять скидка? " +
        "Отправь цифрой в этот чат.\n\n" +
        "Чтобы удалить скидку, отправь в чат цифру <b>0</b>";

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
                InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: $"{callbackDataCode}Back{productId}"),
            },
        });
    }
}
