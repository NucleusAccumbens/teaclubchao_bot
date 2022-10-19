using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class NumberCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly ContactsMessage _contactsMessage;

    public NumberCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _contactsMessage = new(_getUserLanguageQuery);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'Y';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (orderDto != null && orderDto.Contacts != null)
            {
                orderDto.Contacts.Name = null;

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                _memoryCachService.SetMemoryCach(chatId, "userName");

                await _contactsMessage.GetMessage(chatId, messageId, client);
            }
        }
    }
}
