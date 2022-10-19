using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.HerbMessages;

public class HerbRegionForMenuMessage
{
    private readonly string _russianMessageText =
        "🌱  Испокон веков люди использовали травы для приготовления напитков, профилактики и лечения недугов. " +
        "Различные отвары, настойки и чаи приносят пользу для организма человека, повышая иммунитет и самочувствие. " +
        "Однако не стоит считать их панацеей от всех болезней, но то, что там есть много антиоксидантов и витаминов, " +
        "не оспорит даже самый убежденный скептик 🌱\n\n" +
        "⛩ Чайный Автономный Округ ⛩ собрал лучшие травы из разных регионов! " +
        "Выбери регион и ознакомься с ассортиментом.";

    private readonly string _englishMessageText =
        "🌱 From time immemorial, people have used herbs to make drinks, prevent and treat ailments." +
        "Various decoctions, tinctures and teas are beneficial for the human body, increasing immunity and well-being. " +
        "However, you should not consider them a panacea for all diseases, but the fact that there are many antioxidants and vitamins," +
        "even the most convinced skeptic will not dispute 🌱\n\n" +
        "⛩ Tea Autonomous Prefecture ⛩ has collected the best herbs from different regions!" +
        "Choose a region and get acquainted with the assortment.";

    private readonly string _hebrewMessageText =
        "🌱 מאז ומתמיד, אנשים השתמשו בצמחי מרפא להכנת משקאות, מניעה וטיפול במחלות." +
        "מרתחים שונים, תמיסות ותה מועילים לגוף האדם, מגבירים את החסינות והרווחה." +
        "עם זאת, אתה לא צריך לראות בהם תרופת פלא לכל המחלות, אלא העובדה שיש הרבה נוגדי חמצון וויטמינים," +
        "אפילו הספקן המשוכנע ביותר לא יערער 🌱\n\n" +
        "המחוז האוטונומי של תה ⛩ אספה את מיטב עשבי התיבול מאזורים שונים!" +
        "בחרו איזור ותכירו את המבחר.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🏔️ Алтай 🏔️", callbackData: "OAltai"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌲 Карелия 🌲", callbackData: "OKarelia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛰️ Кавказ ⛰️", callbackData: "OCaucasus"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🗻 Сибирь 🗻", callbackData: "OSiberia"),
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
            InlineKeyboardButton.WithCallbackData(text: "🏔️ Altai 🏔️", callbackData: "OAltai"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌲 Karelia 🌲", callbackData: "OKarelia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛰️ Caucasus ⛰️", callbackData: "OCaucasus"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🗻 Siberia 🗻", callbackData: "OSiberia"),
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
            InlineKeyboardButton.WithCallbackData(text: "🏔️ Altai 🏔️", callbackData: "OAltai"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌲 Karelia 🌲", callbackData: "OKarelia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛰️ Caucasus ⛰️", callbackData: "OCaucasus"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🗻 Siberia 🗻", callbackData: "OSiberia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Back", callbackData: "*Menu"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public HerbRegionForMenuMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
