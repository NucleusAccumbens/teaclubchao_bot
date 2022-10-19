using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.HerbMessages;

public class HerbBuyerPageMessage
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public HerbBuyerPageMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }


    public async Task GetMessage(long chatId, ITelegramBotClient client, HerbDto herbDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        var inlineKeyboardMarkup = GetInlineKeyboardMarkup(language, herbDto.Id);

        if (herbDto.PathToPhoto != null)
        {

            if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
                HerbStringBuilder.GetStringForHerb(herbDto, language), herbDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.English) await MessageService.SendMessage(chatId, client,
                HerbStringBuilder.GetStringForHerb(herbDto, language), herbDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
                HerbStringBuilder.GetStringForHerb(herbDto, language), herbDto.PathToPhoto, inlineKeyboardMarkup);
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
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: $"SHerbAddToCard{id}"),
                },
            });
        }

        if (language == Language.English)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"SHerbAddToCard{id}"),
                },
            });
        }

        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"SHerbAddToCard{id}"),
            },
        });
    }
}
