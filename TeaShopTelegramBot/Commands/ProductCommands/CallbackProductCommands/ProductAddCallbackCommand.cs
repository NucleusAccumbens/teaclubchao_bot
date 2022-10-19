using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.ProductCommands.CallbackProductCommands;

public class ProductAddCallbackCommand : BaseCallbackCommand
{
    private readonly AddProductMessage _productAddMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    public ProductAddCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => '.';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {

            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            _memoryCachService.SetMemoryCach(String.Empty, update);

            await _productAddMessage.GetMessage(chatId, messageId, client);

            return;
        }
    }
}

