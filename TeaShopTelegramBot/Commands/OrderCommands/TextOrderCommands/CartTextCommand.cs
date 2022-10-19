using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.TextOrderCommands;

public class CartTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;
    
    private readonly CartMessage _cartMessage;

    public CartTextCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _cartMessage = new(_getUserLanguageQuery);
        _memoryCachService = memoryCachService;
    }

    public override string Name => "/cart";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (orderDto == null)
            {
                orderDto = new Models.OrderDto() { UserChatId = chatId };

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                await _cartMessage.GetMessage(chatId, client, orderDto);

                return;
            }

            await _cartMessage.GetMessage(chatId, client, orderDto);
        }
    }
}
