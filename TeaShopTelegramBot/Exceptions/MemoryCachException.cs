namespace TeaShopTelegramBot.Exceptions;
public class MemoryCachException : Exception
{
    private readonly string _text = "Прошло слишком много времени с предыдущего сеанса, " +
        "данные внесённые ранее не сохранились.\n\n" +
        "Чтобы начать сначала, выберите команду:\n\n" +
        "/start - начало работы\n";

    public MemoryCachException()
        : base()
    {
    }

    public async Task SendExceptionMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService.SendMessage(chatId, client, _text, null);
    }
}
