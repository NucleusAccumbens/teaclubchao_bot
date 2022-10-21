using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.HerbMessages;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class HerbRegionsForEditCallbackCommand : BaseCallbackCommand
{
    private readonly HerbRegionsForEditMessage _herbRegionsForEditMessage = new();

    public override char CallbackDataCode => 'f';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _herbRegionsForEditMessage.GetMessage(chatId, messageId, client);
        }
    }
}
