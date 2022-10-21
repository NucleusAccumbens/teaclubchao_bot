using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.ProductMessages;

public class RemoveProductMessage
{
    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, ProductDto productDto, char callbackDataCode)
    {
        string messageText = $"<i><b>ТЫ ДЕЙСТВИТЕЛЬНО ХОЧЕШЬ УДАЛИТЬ {productDto.Name} ИЗ БАЗЫ ДАННЫХ?</b></i>\n\n";

        if (productDto is TeaDto) messageText += TeaStringBuilder.GetStringForTea(productDto as TeaDto, Language.Russian);

        if (productDto is HerbDto) messageText += HerbStringBuilder.GetStringForHerb(productDto as HerbDto, Language.Russian);

        if (productDto is HoneyDto) messageText += HoneyStringBuilder.GetStringForHoney(productDto as HoneyDto, Language.Russian);

        await MessageService.EditMessage(chatId, messageId, client,
            messageText, GetInlineKeyboardMarkup(productDto.Id, callbackDataCode));
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long? productId, char callbackDataCode)
    {
        return new(new[]
        {
            new[]
            {                
                InlineKeyboardButton.WithCallbackData(text: "❌ Да, удалить", callbackData: $"{callbackDataCode}Remove{productId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: $"{callbackDataCode}Back{productId}"),
            },
        });
    }
}
