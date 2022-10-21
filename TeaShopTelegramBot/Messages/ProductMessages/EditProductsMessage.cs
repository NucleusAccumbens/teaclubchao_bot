namespace TeaShopTelegramBot.Messages.ProductMessages;

public class EditProductsMessage
{
    private readonly string _messageText =
        "Чтобы перейти к <b><i>редактированию</i></b>, выбери категорию товара.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Чай 🍃", callbackData: "eTeaTypes"),
            InlineKeyboardButton.WithCallbackData(text: "🌱 Травы 🌱", callbackData: "fHerbRegions")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍯 Мёд 🍯", callbackData: "gHoneyWeights"),
        },
    });

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        await MessageService.EditMessage(chatId, messageId, client,
            _messageText, _inlineKeyboardMarkup);
    }

    public async Task GetMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService.SendMessage(chatId, client,
            _messageText, _inlineKeyboardMarkup);
    }
}
