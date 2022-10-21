using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.AdminMessages.TeaMessages;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaAddCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;
    
    private readonly TeaTypeForAddMessage _teaTypeMessage = new();

    public TeaAddCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'A';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            _memoryCachService.SetMemoryCach(String.Empty, update);

            await _teaTypeMessage.GetMessage(chatId, messageId, client);

            return;
        }        
    }
}
