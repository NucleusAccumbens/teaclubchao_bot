using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class MenuTextCommand : BaseTextCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;
    
    private readonly MenuMessage _menuMessage;

    public MenuTextCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _menuMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "/menu";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            await _menuMessage.GetMessage(update.Message.Chat.Id, client);
        }
    }
}
