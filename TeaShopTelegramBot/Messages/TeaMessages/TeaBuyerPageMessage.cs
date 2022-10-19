using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaBuyerPageMessage
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public TeaBuyerPageMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }


    public async Task GetMessage(long chatId, ITelegramBotClient client, TeaDto teaDto)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        var inlineKeyboardMarkup = GetInlineKeyboardMarkup(language, teaDto.Id);

        if (teaDto.PathToPhoto != null)
        {

            if (language == Language.Russian) await MessageService.SendMessage(chatId, client, 
                TeaStringBuilder.GetStringForTea(teaDto, language), teaDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.English) await MessageService.SendMessage(chatId, client,
                TeaStringBuilder.GetStringForTea(teaDto, language), teaDto.PathToPhoto, inlineKeyboardMarkup);

            if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
                TeaStringBuilder.GetStringForTea(teaDto, language), teaDto.PathToPhoto, inlineKeyboardMarkup);
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
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: $"RTeaAddToCard{id}"),
                },
            });
        }

        if (language == Language.English)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"RTeaAddToCard{id}"),
                },
            });
        }

        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🛒 Add to cart 🛒", callbackData: $"RTeaAddToCard{id}"),
            },
        });
    }
}
