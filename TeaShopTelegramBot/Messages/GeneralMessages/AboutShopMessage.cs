using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class AboutShopMessage
{
    private readonly string _russianMessageText = "Создатель ⛩ Чайного Автономного Округа ⛩ Алексей с 2011 года занимается китайскими чаями!\n" +
        "Он поставил себе задачей привозить качественный пуэр по доступной цене.\n\n" +
        "Алексей 8 раз был в Китае, и теперь может сказать, что разбирается в чае!";

    private readonly string _englishMessageText = "The creator of ⛩ Tea Autonomous Region ⛩ Aleksey has been making Chinese teas since 2011!\n" +
        "He made it his mission to bring quality pu-erh at an affordable price.\n\n" +
        "Aleksey has been to China 8 times, and now he can say that he understands tea!";

    private readonly string _hebrewMessageText = "היוצר של ⛩ האזור האוטונומי של תה ⛩ אלכסי מכין תה סיני מאז 2011!\n" +
         "הוא עשה זאת למשימתו להביא pu-erh איכותי במחיר סביר.\n\n" +
         "אלכסי היה בסין 8 פעמים, ועכשיו הוא יכול לומר שהוא מבין תה!";

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

    public AboutShopMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
