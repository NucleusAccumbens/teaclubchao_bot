using Application.Honeys.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class GetHoneyForEditCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetHoneyQuery _getHoneyQuery;

    public GetHoneyForEditCallbackCommand(IGetHoneyQuery getHoneyQuery)
    {
        _getHoneyQuery = getHoneyQuery;
    }

    public override char CallbackDataCode => 'g';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            var allHoney = await _getHoneyQuery.GetAllHoneyAsync();
            
            foreach (var tea in allHoney)
            {
                await _productEditPageMessage.GetMessage(chatId, client,
                    tea.Adapt<HoneyDto>(), 'p');
            }

            return;
        }
    }
}
