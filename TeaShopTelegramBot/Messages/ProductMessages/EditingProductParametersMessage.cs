using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.ProductMessages;

public class EditingProductParametersMessage
{
    private readonly string _editDescriptionTextMessage = "Отправь описание сообщением в этот чат.";

    private readonly string _editCountTextMessage = "Отправь количество сообщением в этот чат.";

    private readonly string _editPriceTextMessage = "Отправь цену сообщением в этот чат.";

    public async Task GetMessageToEditDescription(long chatId, int messageId, ITelegramBotClient client, long productId, char callbackDataCode)
    {
        await MessageService.EditMessage(chatId, messageId, client,
            _editDescriptionTextMessage, GetInlineKeyboardMarkup(productId, callbackDataCode));
    }

    public async Task GetMessageToEditCount(long chatId, int messageId, ITelegramBotClient client, long productId, char callbackDataCode)
    {
        await MessageService.EditMessage(chatId, messageId, client,
           _editCountTextMessage, GetInlineKeyboardMarkup(productId, callbackDataCode));
    }

    public async Task GetMessageToEditPrice(long chatId, int messageId, ITelegramBotClient client, long productId, char callbackDataCode)
    {
        await MessageService.EditMessage(chatId, messageId, client,
            _editPriceTextMessage, GetInlineKeyboardMarkup(productId, callbackDataCode));
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long? productId, char callbackDataCode)
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: $"{callbackDataCode}BackToParams{productId}"),
            },
        });
    }
}
