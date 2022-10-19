namespace TeaShopTelegramBot.Messages.AdminMessages.TeaMessages;

public class TeaTypeForAddMessage
{
    private readonly string _messageText = "Выбери сорт чая, который собираешься добавить.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌹  Красный  🌹", callbackData: "FRed"),
            InlineKeyboardButton.WithCallbackData(text: "🍃  Зелёный  🍃", callbackData: "FGreen"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐚  Белый  🐚", callbackData: "FWhite"),
            InlineKeyboardButton.WithCallbackData(text: "🐉  Улун  🐉", callbackData: "FOloong"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐲  Шу пуэр  🐲", callbackData: "FShuPuer"),
            InlineKeyboardButton.WithCallbackData(text: "🌚  Шен пуэр  🌚", callbackData: "FShenPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🦚  Авторский чай  🦚", callbackData: "FCraft"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ".AddProduct")
        },
    });

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        await MessageService.EditMessage(chatId, messageId, client,
            _messageText, _inlineKeyboardMarkup);
    }
}
