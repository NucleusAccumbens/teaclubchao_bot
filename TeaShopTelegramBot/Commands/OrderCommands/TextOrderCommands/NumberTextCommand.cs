using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.TextOrderCommands;

public class NumberTextCommand : BaseTextCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly AddressMessage _addressMessage;


    public NumberTextCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _addressMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "number";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string number = update.Message.Text;

            try
            {
                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch(chatId);

                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                _memoryCachService.SetMemoryCach(chatId, "address");

                if (orderDto != null && orderDto.Contacts != null)
                {
                    orderDto.Contacts.Number = number;

                    await MessageService
                        .DeleteMessage(chatId, update.Message.MessageId, client);

                    await _addressMessage
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
