using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralCallbackCommands;

public class InfoCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly AboutShopMessage _aboutShopMessage;

    private readonly ContactsMessage _contactsMessage;

    public InfoCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _aboutShopMessage = new(_getUserLanguageQuery);
        _contactsMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => '+';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            if (update.CallbackQuery.Data == "+AboutShop")
            {
                await _aboutShopMessage.GetMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "+Contact")
            {
                await _contactsMessage.GetMessage(chatId, messageId, client);
            }
        }
    }
}
