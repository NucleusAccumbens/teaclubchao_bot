using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.HerbMessages;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class HerbRegionsForMenuCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly HerbRegionForMenuMessage _herbRegionForMenuMessage;

    public HerbRegionsForMenuCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _herbRegionForMenuMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'P';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _herbRegionForMenuMessage.GetMessage(chatId, messageId, client);
        }
    }
}
