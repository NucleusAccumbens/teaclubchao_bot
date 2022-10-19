using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.HoneyMessages;

public class HoneyBuyerPageMessage
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public HoneyBuyerPageMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }


    public async Task GetMessage(long chatId, ITelegramBotClient client, HoneyDto honeyDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        var inlineKeyboardMarkup = GetInlineKeyboardMarkup(language, honeyDto.Id);

        if (honeyDto.PathToPhoto != null)
        {

            if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
                HoneyStringBuilder.GetStringForHoney(honeyDto, language), honeyDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.English) await MessageService.SendMessage(chatId, client,
                HoneyStringBuilder.GetStringForHoney(honeyDto, language), honeyDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
                HoneyStringBuilder.GetStringForHoney(honeyDto, language), honeyDto.PathToPhoto, inlineKeyboardMarkup);
        }
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(Language? language, long? id)
    {
        if (language == Language.Russian)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: $"THoneyAddToCard{id}"),
                },
            });
        }

        if (language == Language.English)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"THoneyAddToCard{id}"),
                },
            });
        }

        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"THoneyAddToCard{id}"),
            },
        });
    }
}
