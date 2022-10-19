using Application.TlgUsers.Interfaces;

namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class NavigationMessage
{
    private readonly string _russianTextForMessage =
        "Чтобы выбрать что-то из других категорий, переходи в <b>\"✨ Меню ✨\"<\b>\n\n" +
        "Чтобы подтвердить заказ, переходи в <b>\"🛒 Корзина 🛒\"<\b>";

    private readonly string _englishTextForMessage =
        "To choose something from other categories, go to <b>\"✨ Menu ✨\"<\b>\n\n" +
        "To confirm your order, go to <b>\"🛒 Cart 🛒\"<\b>";

    private readonly string _hebrewTextForMessage =
        "כדי לבחור משהו מקטגוריות אחרות, עבור אל <b>\"✨ תפריט ✨\"<\b>\n\n" +
         "כדי לאשר את ההזמנה שלך, עבור אל <b>\"🛒 Cart 🛒\"<\b>";

    private readonly InlineKeyboardMarkup _russianInlineKeyvboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "*Menu*"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "/Cart"),
        }
    });


    private readonly InlineKeyboardMarkup _englishInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Menu ✨", callbackData: "*Menu*"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
    });


    private readonly InlineKeyboardMarkup _hebrevInlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✨ Menu ✨", callbackData: "*Menu*"),
            InlineKeyboardButton.WithCallbackData(text: "🛒 Cart 🛒", callbackData: "/Cart"),
        },
    });

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public NavigationMessage(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public async Task GetMessage(long chatId, ITelegramBotClient client)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService.SendMessage(chatId, client,
            _russianTextForMessage, _russianInlineKeyvboardMarkup);

        if (language == Language.English) await MessageService.SendMessage(chatId, client,
            _englishTextForMessage, _englishInlineKeyboardMarkup);

        if (language == Language.Hebrew) await MessageService.SendMessage(chatId, client,
            _hebrewTextForMessage, _hebrevInlineKeyboardMarkup);
    }
}
