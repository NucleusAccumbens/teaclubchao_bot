using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaTypesForMenuMessage
{
    private readonly string _russianMessageText = 
        "🍃  Существует всего 6 основных сортов чая, которые производятся из чайного листа: " +
        "черный, зеленый, белый, желтый, улун и пуэр. " +
        "Друг от друга они отличаются степенью ферментации и способом обработки, " +
        "растение же всегда одно – Camellia sinensis, представляющая собой куст или дерево 🍃\n\n" +
        "⛩ Чайный Автономный Округ ⛩ предлагает разнообразие сортов! " +
        "Выбери сорт чая, который подойдёт именно тебе.";

    private readonly string _englishMessageText = 
        "🍃 There are only 6 main varieties of tea that are made from tea leaves: " +
        "black, green, white, yellow, oolong and puer. " +
        "They differ from each other in the degree of fermentation and the way they are processed, " +
        "the plant is always the same - Camellia sinensis, which is a bush or a tree 🍃\n\n" +
        "⛩ Tea Autonomous Region ⛩ offers a variety of varieties!" +
        "Choose the type of tea that suits you.";

    private readonly string _hebrewMessageText =
        "🍃 ישנם רק 6 זנים עיקריים של תה העשויים מעלי תה: " +
         "שחור, ירוק, לבן, צהוב, אולונג ופואר." +
         "הם נבדלים זה מזה במידת התסיסה ובאופן העיבוד שלהם," +
         "הצמח הוא תמיד אותו הדבר - Camellia sinensis, שהוא שיח או עץ 🍃\n\n" +
         "האזור האוטונומי של תה ⛩ מציע מגוון של זנים!" +
         "בחר את סוג התה המתאים לך.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌹 Красные 🌹", callbackData: "NRed"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Зелёные 🍃", callbackData: "NGreen"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐚 Белые 🐚", callbackData: "NWhite"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐉 Улуны 🐉", callbackData: "NOolong"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐲 Шу Пуэры 🐲", callbackData: "NShuPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌚 Шен Пуэры 🌚", callbackData: "NShenPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🦚  Авторские чаи  🦚", callbackData: "NCraft"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "*Menu"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌹 Red 🌹", callbackData: "NRed"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Green 🍃", callbackData: "NGreen"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐚 White 🐚", callbackData: "NWhite"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐉 Oloong 🐉", callbackData: "NOolong"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐲 Shu puer 🐲", callbackData: "NShuPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌚 Shen puer 🌚", callbackData: "NShenPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🦚  Craft teas  🦚", callbackData: "NCraft"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "*Menu"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌹 Red 🌹", callbackData: "NRed"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Green 🍃", callbackData: "NGreen"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐚 White 🐚", callbackData: "NWhite"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐉 Oloong 🐉", callbackData: "NOolong"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐲 Shu puer 🐲", callbackData: "NShuPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌚 Shen puer 🌚", callbackData: "NShenPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🦚  Craft teas  🦚", callbackData: "NCraft"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "*Menu"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public TeaTypesForMenuMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
