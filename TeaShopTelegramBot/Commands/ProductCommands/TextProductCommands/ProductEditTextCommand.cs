using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.ProductCommands.TextProductCommands;

public class ProductEditTextCommand : BaseTextCommand
{
    private readonly EditProductsMessage _editProductsMessage = new();
    
    public override string Name => "/edit_products";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            await _editProductsMessage.GetMessage(chatId, client);
        }
    }
}
