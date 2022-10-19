using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class LanguageTextCommand : BaseTextCommand
{
    private readonly LanguageMessage _languageMessage = new();

    public override string Name => "/language";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            await _languageMessage.GetMessage(chatId, client);
        }
    }
}
