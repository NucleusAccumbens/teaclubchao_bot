using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.ProductCommands.TextProductCommands;

public class ProductAddTextCommand : BaseTextCommand
{
    private readonly AddProductMessage _productAddMessage = new();

    public override string Name => "/add_product";

    public async override Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            await _productAddMessage.GetMessage(chatId, client);

            return;
        }
    }
}