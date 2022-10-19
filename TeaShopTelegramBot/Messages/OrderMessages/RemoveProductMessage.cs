using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class RemoveProductMessage
{
    private readonly ProductStringBuilder _stringBuilder = new();

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public RemoveProductMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, ITelegramBotClient client, ProductDto productDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        var text = _stringBuilder.GetStringForProducts(productDto);

        var inlineKeyboardMarkup = GetInlineKeyboardMarkup(language, productDto.Id);

        if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
            text, inlineKeyboardMarkup);

        if (language == Language.English) await MessageService.SendMessage(chatId, client,
            text, inlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
            text, inlineKeyboardMarkup);
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(Language? language, long? productId)
    {
        if (language == Language.Russian)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"ZRemove{productId}"),
                },
            });
        }

        if (language == Language.English)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "❌ Remove", callbackData: $"ZRemove{productId}"),
                },
            });
        }

        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "❌ Remove", callbackData: $"ZRemove{productId}"),
            },
        });
    }
}
