namespace TeaShopTelegramBot.Messages.HerbMessages;

public class HerbRegionsForEditMessage
{
    private readonly string _messageText = "Чтобы перейти к редактированию трав, выбери регион.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🏔️ Алтай 🏔️", callbackData: "lAltai"),
            InlineKeyboardButton.WithCallbackData(text: "🌲 Карелия 🌲", callbackData: "lKarelia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛰️ Кавказ ⛰️", callbackData: "lCaucasus"),
            InlineKeyboardButton.WithCallbackData(text: "🗻 Сибирь 🗻", callbackData: "lSiberia"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "%EditProduct")
        },
    });

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        await MessageService.EditMessage(chatId, messageId, client,
            _messageText, _inlineKeyboardMarkup);
    }
}
