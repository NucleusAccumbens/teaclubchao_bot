using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.ProductMessages;

public class ProductEditPageMessage
{
    public async Task GetMessage(long chatId, ITelegramBotClient client, ProductDto productDto, char callbackDataCode)
    {
        string text = String.Empty;

        if (productDto is TeaDto) text += TeaStringBuilder.GetStringForTea(productDto as TeaDto, Language.Russian);

        if (productDto is HerbDto) text += HerbStringBuilder.GetStringForHerb(productDto as HerbDto, Language.Russian);

        if (productDto is HoneyDto) text += HoneyStringBuilder.GetStringForHoney(productDto as HoneyDto, Language.Russian);

        await MessageService.SendMessage(chatId, client,
            text, GetInlineKeyboardMarkup(productDto.Id, callbackDataCode));
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, ProductDto productDto, char callbackDataCode)
    {
        string text = String.Empty;

        if (productDto is TeaDto) text += TeaStringBuilder.GetStringForTea(productDto as TeaDto, Language.Russian);

        if (productDto is HerbDto) text += HerbStringBuilder.GetStringForHerb(productDto as HerbDto, Language.Russian);

        if (productDto is HoneyDto) text += HoneyStringBuilder.GetStringForHoney(productDto as HoneyDto, Language.Russian);

        await MessageService.EditMessage(chatId, messageId, client,
            text, GetInlineKeyboardMarkup(productDto.Id, callbackDataCode));
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long? productId, char callbackDataCode)
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🖊 Редактировать", callbackData: $"{callbackDataCode}Edit{productId}"),
                InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"{callbackDataCode}Remove{productId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔖 Сделать скидку", callbackData: $"{callbackDataCode}Discount{productId}"),
            },
        });
    }
}
