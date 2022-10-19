using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.TeaMessages;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaTypeForEditCallbackCommand : BaseCallbackCommand
{
    private readonly TeaTypeForEditMessage _teaTypeForEditMessage = new();
    
    public override char CallbackDataCode => 'e';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _teaTypeForEditMessage.GetMessage(chatId, messageId, client);
        }
    }
}
