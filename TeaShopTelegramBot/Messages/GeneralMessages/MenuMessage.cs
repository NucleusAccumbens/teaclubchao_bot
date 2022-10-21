using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class MenuMessage
{
    private readonly string _russianMessageText = $"Выбери категорию.";

    private readonly string _englishMessageText = $"Choose a category.";

    private readonly string _hebrewMessageText = $"בחר קטגוריה.";

    private readonly InlineKeyboardMarkup _russianInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Чай 🍃", callbackData: "MTea"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌱 Травы 🌱", callbackData: "PHerb"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍯 Мёд 🍯", callbackData: "QHoney"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🎁  Скидки  🎁", callbackData: "№Discount"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛩ О магазине", callbackData: "+AboutShop"),
            InlineKeyboardButton.WithCallbackData(text: "📝 Контакты", callbackData: "+Contact"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "/Cart"),
        },
    });

    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Tea 🍃", callbackData: "MTea"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌱 Herb 🌱", callbackData: "PHerb"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍯 Honey 🍯", callbackData: "QHoney"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🎁 Discount 🎁", callbackData: ":Discount"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛩ About store", callbackData: "+AboutShop"),
            InlineKeyboardButton.WithCallbackData(text: "📝 Contacts", callbackData: "+Contact"),
        },
    });

    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Tea 🍃", callbackData: "MTea"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌱 Herb 🌱", callbackData: "PHerb"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍯 Honey 🍯", callbackData: "QHoney"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🎁 Discount 🎁", callbackData: ":Discount"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛩ About store", callbackData: "+AboutShop"),
            InlineKeyboardButton.WithCallbackData(text: "📝 Contacts", callbackData: "+Contact"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public MenuMessage(IGetUserLanguageQuery getUserLanguageQuery)
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
