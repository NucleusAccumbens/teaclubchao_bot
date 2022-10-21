using Application.TlgUsers.Interfaces;
using System.ComponentModel.DataAnnotations;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralCallbackCommands;

public class MenuCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly MenuMessage _menuMessage;

    public MenuCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _menuMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => '*';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            if (update.CallbackQuery.Data == "*Menu")
            {
                await _menuMessage.GetMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "*Menu*")
            {
                await _menuMessage.GetMessage(chatId, client);
            }
        }
    }
}
