namespace TeaShopTelegramBot.Exceptions;

public class TryParseException : Exception
{
    public TryParseException()
        : base()
    {
    }

    public async Task GetAnswerForTryParseException(ITelegramBotClient client, string callbackQueryId)
    {

        await client.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQueryId,
            text: $"‼️ Цена может быть только числом ‼️\n\n" +
            $"Попробуй установить цену снова.",
            showAlert: true);
    }
}
