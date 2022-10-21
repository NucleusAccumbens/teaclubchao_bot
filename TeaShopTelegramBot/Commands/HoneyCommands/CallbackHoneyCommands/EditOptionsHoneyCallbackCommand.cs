using Application.Honeys.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class EditOptionsHoneyCallbackCommand : BaseCallbackCommand
{
    private readonly RemoveProductMessage _removeProductMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly DiscountMessage _discountMessage = new();

    private readonly IGetHoneyQuery _getHoneyQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditOptionsHoneyCallbackCommand(IGetHoneyQuery getHoneyQuery, IMemoryCachService memoryCachService)
    {
        _getHoneyQuery = getHoneyQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'p';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("pEdit"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client, GetHoneyIdForEdit(data), 'q');

                return;
            }
            if (update.CallbackQuery.Data.Contains("pRemove"))
            {
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForRemove(data));

                await _removeProductMessage.GetMessage(chatId, messageId, client, honey.Adapt<HoneyDto>(), 'r');

                return;
            }
            if (update.CallbackQuery.Data.Contains("pDiscount"))
            {
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForDiscount(data));

                _memoryCachService.SetMemoryCach(honey.Adapt<HoneyDto>());

                _memoryCachService.SetMemoryCach("honeyDiscount", update);

                await _discountMessage.GetMessage(chatId, messageId, client, honey.Id, 'q');
            }
        }
    }

    private long GetHoneyIdForEdit(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetHoneyIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetHoneyIdForDiscount(string data)
    {
        return Convert.ToInt64(data[9..]);
    }
}
