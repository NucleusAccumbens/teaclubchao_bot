using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class NumberMessage
{
    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "YGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "YGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "YGoBack"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public NumberMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, string? name)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, name), _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, name), _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, name), _hebrevInlineKeyboardMarkup);
    }

    private string GetMessage(Language? language, string? name)
    {
        if (language == Language.Russian) return 
                $"<b>ФИО:</b> {name}\n\n" +
                $"Отправь номер телефона сообщением в этот чат.";

        if (language == Language.English) return
                $"<b>Name:</b> {name}\n\n" +
                $"Send your phone number as a message to this chat.";

        return $"שלח את מספר הטלפון שלך כהודעה לצ'אט זה.";
    }
}
