using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.TextOrderCommands;

public class AddressTextCommand : BaseTextCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly SaveContactsMessage _saveContactsMessage;


    public AddressTextCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _saveContactsMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "address";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string address = update.Message.Text;

            try
            {
                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch(chatId);

                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                _memoryCachService.SetMemoryCach(chatId, String.Empty);

                if (orderDto != null && orderDto.Contacts != null)
                {
                    orderDto.Contacts.Address = address;

                    await MessageService
                        .DeleteMessage(chatId, update.Message.MessageId, client);

                    await _saveContactsMessage
                        .GetMessage(chatId, messageId, client, orderDto.Contacts);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
