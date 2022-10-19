namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaTypeForEditMessage
{
    private readonly string _messageText = "Чтобы перейти к редактированию чая, выбери сорт.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌹  Красный  🌹", callbackData: "hRed"),
            InlineKeyboardButton.WithCallbackData(text: "🍃  Зелёный  🍃", callbackData: "hGreen"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐚  Белый  🐚", callbackData: "hWhite"),
            InlineKeyboardButton.WithCallbackData(text: "🐉  Улун  🐉", callbackData: "hOloong"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🐲  Шу пуэр  🐲", callbackData: "hShuPuer"),
            InlineKeyboardButton.WithCallbackData(text: "🌚  Шен пуэр  🌚", callbackData: "hShenPuer"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🦚  Авторский чай  🦚", callbackData: "hCraft"),
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
