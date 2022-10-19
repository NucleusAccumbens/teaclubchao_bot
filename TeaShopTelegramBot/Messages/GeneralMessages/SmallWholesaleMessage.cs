using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class SmallWholesaleMessage
{
    private readonly string _russianMessageText =
       "✨ В нашем магазине доступен мелкий опт ✨\n\n" +
       "Чтобы сделать заказ, напишите администратору.";

    private readonly string _englishMessageText =
        "✨ Small wholesale is available in our store ✨\n\n" +
        "To place an order, write to the administrator.";

    private readonly string _hebrewMessageText =
        "✨ סיטונאי קטן זמין בחנות שלנו ✨\n\n" +
        "כדי לבצע הזמנה, כתוב למנהל המערכת.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Связаться с администратором 💬", url: "http://t.me/shanti_travels"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Contact administrator 💬", url: "http://t.me/shanti_travels"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "Contact administrator 💬", url: "http://t.me/shanti_travels"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public SmallWholesaleMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, ITelegramBotClient client)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
            _russianMessageText, _russianInlineKeyboardMarkup);

        if (language == Language.English) await MessageService.SendMessage(chatId, client,
            _englishMessageText, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
            _hebrewMessageText, _hebrevInlineKeyboardMarkup);
    }
}
