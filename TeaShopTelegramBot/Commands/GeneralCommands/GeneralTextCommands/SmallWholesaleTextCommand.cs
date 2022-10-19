using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class SmallWholesaleTextCommand : BaseTextCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly SmallWholesaleMessage _smallWholesaleMessage;

    public SmallWholesaleTextCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _smallWholesaleMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "/small_wholesale";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            var chatId = update.Message.Chat.Id;

            await _smallWholesaleMessage.GetMessage(chatId, client);
        }
    }
}
