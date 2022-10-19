namespace TeaShopTelegramBot.Messages.ProductMessages;

public class AddProductMessage
{
    private readonly string _messageText = 
        "Товар какой категории хочешь <b><i>дабавить</i></b>? Выбери, нажав на кнопку ниже.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍃 Чай 🍃", callbackData: "ATeaTypes"),
            InlineKeyboardButton.WithCallbackData(text: "🌱 Травы 🌱", callbackData: "BHerbRegions")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🍯 Мёд 🍯", callbackData: "CHoneyWeights"),
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
