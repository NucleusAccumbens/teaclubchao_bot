using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class SaveContactsMessage
{
    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✅ Сохранить контакты", callbackData: "bSave"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "bGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✅ Save contacts", callbackData: "bSave"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "bGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✅ Сохранить контакты", callbackData: "bSave"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "bGoBack"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public SaveContactsMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, ContactsDto contacts)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, contacts), _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, contacts), _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.EditMessage(chatId, messageId, client,
            GetMessage(language, contacts), _hebrevInlineKeyboardMarkup);
    }

    private string GetMessage(Language? language, ContactsDto contacts)
    {
        if (language == Language.Russian) return
                $"<b>ФИО:</b> {contacts.Name}\n" +
                $"<b>Номер телефона:</b> {contacts.Number}\n" +
                $"<b>Адрес пункта выдачи:</b> {contacts.Address}\n\n" +
                $"Проверьте правильность введённых данных. " +
                $"Если всё верно, нажмите кнопку <b>\"Сохранить контакты\"</b>\n\n" +
                $"Чтобы внести изменения, вернитесь к предыдущим шагам.";

        if (language == Language.English) return
                $"<b>Name:</b> {contacts.Name}\n" +
                 $"<b>Phone number:</b> {contacts.Number}\n" +
                 $"<b>Address of point of issue:</b> {contacts.Address}\n\n" +
                 $"Check your input is correct." +
                 $"If everything is correct, press the button <b>\"Save contacts\"</b>\n\n" +
                 $"To make changes, go back to the previous steps.";

        return $"<b>שם:</b> {contacts.Name}\n" +
                 $"<b>מספר טלפון:</b> {contacts.Number}\n" +
                 $"<b>כתובת נקודת הבעיה:</b> {contacts.Address}\n\n" +
                 $"בדוק שהקלט שלך נכון." +
                 $"אם הכל תקין, לחץ על הלחצן <b>\"Save contacts\"</b>\n\n" +
                 $"כדי לבצע שינויים, חזור לשלבים הקודמים.";
    }
}
