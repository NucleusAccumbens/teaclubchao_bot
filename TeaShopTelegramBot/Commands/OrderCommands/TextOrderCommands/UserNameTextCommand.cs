using Application.TlgUsers.Interfaces;
using System.ComponentModel.Design;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.TextOrderCommands;

public class UserNameTextCommand : BaseTextCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly NumberMessage _numberMessage;

    public UserNameTextCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _numberMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "userName";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string name = update.Message.Text;

            try
            {
                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch(chatId);
                
                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                _memoryCachService.SetMemoryCach(chatId, "number");

                if (orderDto != null && orderDto.Contacts != null)
                {
                    orderDto.Contacts.Name = name;

                    await MessageService
                        .DeleteMessage(chatId, update.Message.MessageId, client);

                    await _numberMessage.GetMessage(chatId, messageId, client, name);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
