using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class ContactsMessage
{
    private readonly string _russianMessageText = $"Чтобы оформить доставку, " +
        $"необходимо указать ФИО, номер телефона и адрес пункта выдачи.\n\n" +
        $"Отправь ФИО сообщением в этот чат.";

    private readonly string _englishMessageText = $"To arrange delivery, " +
         $"You must specify your full name, phone number and address of the pickup point.\n\n" +
         $"Message your name to this chat.";

    private readonly string _hebrewMessageText = $"כדי לארגן משלוח, " +
        "עליך לציין את שמך המלא, מספר הטלפון והכתובת של נקודת האיסוף.\n\n" +
        "שלח הודעה עם השם שלך לצ'אט הזה.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "VDeliveryMethodGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "VDeliveryMethodGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "VDeliveryMethodGoBack"),
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
