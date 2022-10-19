using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class ContactsMessage
{
    private readonly string _russianMessageText = 
        "Связаться с администратором: @shanti_travels\n\n" +
        "По вопросам создания бота: @noncredist";

    private readonly string _englishMessageText = 
        "Contact admin: @shanti_travels\n\n" +
        "For questions about creating a bot: @noncredist";

    private readonly string _hebrewMessageText = 
        "צור קשר עם מנהל המערכת: @shanti_travels\n\n" +
         "לשאלות על יצירת בוט: @noncredist";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "*Menu"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "*Menu"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "*Menu"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public ContactsMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            _russianMessageText, _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            _englishMessageText, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            _hebrewMessageText, _hebrevInlineKeyboardMarkup);
    }
}
