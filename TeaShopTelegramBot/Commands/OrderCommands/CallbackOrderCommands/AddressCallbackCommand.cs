using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class AddressCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly NumberMessage _numberMessage;

    public AddressCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _numberMessage = new(_getUserLanguageQuery);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'a';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;            

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (orderDto != null && orderDto.Contacts != null)
            {
                orderDto.Contacts.Number = null;

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                _memoryCachService.SetMemoryCach(chatId, "number");

                await _numberMessage
                    .GetMessage(chatId, messageId, client, orderDto.Contacts.Name);
            }
        }
    }
}
