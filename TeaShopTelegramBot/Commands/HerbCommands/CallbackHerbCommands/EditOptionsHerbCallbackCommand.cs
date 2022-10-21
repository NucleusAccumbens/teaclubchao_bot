using Application.Herbs.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class EditOptionsHerbCallbackCommand : BaseCallbackCommand
{
    private readonly RemoveProductMessage _removeProductMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly DiscountMessage _discountMessage = new();

    private readonly IGetHerbQuery _getHerbQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditOptionsHerbCallbackCommand(IGetHerbQuery getHerbQuery, IMemoryCachService memoryCachService)
    {
        _getHerbQuery = getHerbQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'm';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("mEdit"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client, GetHerbIdForEdit(data), 'n');

                return;
            }
            if (update.CallbackQuery.Data.Contains("mRemove"))
            {
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForRemove(data));

                await _removeProductMessage.GetMessage(chatId, messageId, client, herb.Adapt<HerbDto>(), 'o');

                return;
            }
            if (update.CallbackQuery.Data.Contains("mDiscount"))
            {
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForDiscount(data));

                _memoryCachService.SetMemoryCach(herb.Adapt<HerbDto>());

                _memoryCachService.SetMemoryCach("herbDiscount", update);

                await _discountMessage.GetMessage(chatId, messageId, client, herb.Id, 'n');
            }
        }
    }

    private long GetHerbIdForEdit(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetHerbIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetHerbIdForDiscount(string data)
    {
        return Convert.ToInt64(data[9..]);
    }
}
