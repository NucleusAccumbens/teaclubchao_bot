using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Messages.TeaMessages;

namespace TeaShopTelegramBot.Commands.ProductCommands.CallbackProductCommands;

public class EditProductCallbackCommand : BaseCallbackCommand
{
    private readonly EditProductsMessage _editProductsMessage = new();
    
    public override char CallbackDataCode => '%';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _editProductsMessage.GetMessage(chatId, messageId, client);
        }
    }
}
