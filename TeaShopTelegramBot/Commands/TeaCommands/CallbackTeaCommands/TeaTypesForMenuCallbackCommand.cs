using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.TeaMessages;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaTypesForMenuCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly TeaTypesForMenuMessage _teaTypesForMenuMessage;

    public TeaTypesForMenuCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _teaTypesForMenuMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'M';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            await _teaTypesForMenuMessage.GetMessage(update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId, client);
        }
    }
}
