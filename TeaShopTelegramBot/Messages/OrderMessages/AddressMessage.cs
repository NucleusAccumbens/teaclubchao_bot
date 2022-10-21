using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class AddressMessage
{
    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "aGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "aGoBack"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "aGoBack"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public AddressMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
                $"<b>Номер телефона:</b> {contacts.Number}\n\n" +
                $"Отправь адрес сообщением в этот чат.";

        if (language == Language.English) return
                $"<b>Name:</b> {contacts.Name}\n" +
                 $"<b>Phone number:</b> {contacts.Number}\n\n" +
                 $"Send the address as a message to this chat.";

        return $"<b>שם:</b> {contacts.Name}\n" +
                 $"<b>מספר טלפון:</b> {contacts.Number}\n\n" +
                 $"שלח את הכתובת כהודעה לצ'אט הזה.";
    }
}
