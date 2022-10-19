using Application.Honeys.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class EditingHoneyParametrsCallbackCommand : BaseCallbackCommand
{
    private readonly EditingProductParametersMessage _editingProductParametersMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetHoneyQuery _getHoneyQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditingHoneyParametrsCallbackCommand(IGetHoneyQuery getHoneyQuery, IMemoryCachService memoryCachService)
    {
        _getHoneyQuery = getHoneyQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'q';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("qBackToParams"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client,
                        GetHoneyIdForGoBackToParams(data), 'q');

                return;
            }
            if (update.CallbackQuery.Data.Contains("qBack"))
            {
                _memoryCachService.SetMemoryCach(String.Empty, update);
                
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForGoBack(data));

                if (honey != null)
                {
                    await _productEditPageMessage.GetMessage(chatId, messageId, client,
                        honey.Adapt<HoneyDto>(), 'p');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("qDescription"))
            {
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForDescription(data));

                if (honey != null)
                {
                    _memoryCachService.SetMemoryCach(honey.Adapt<HoneyDto>());

                    _memoryCachService.SetMemoryCach("editHoneyDescription", update);

                    await _editingProductParametersMessage.GetMessageToEditDescription(chatId, messageId,
                        client, honey.Id, 'q');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("qCount"))
            {
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForCount(data));

                if (honey != null)
                {
                    _memoryCachService.SetMemoryCach(honey.Adapt<HoneyDto>());

                    _memoryCachService.SetMemoryCach("editHoneyCount", update);

                    await _editingProductParametersMessage.GetMessageToEditCount(chatId, messageId,
                        client, honey.Id, 'q');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("qPrice"))
            {
                var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyIdForPrice(data));

                if (honey != null)
                {
                    _memoryCachService.SetMemoryCach(honey.Adapt<HoneyDto>());

                    _memoryCachService.SetMemoryCach("editHoneyPrice", update);

                    await _editingProductParametersMessage.GetMessageToEditPrice(chatId, messageId,
                        client, honey.Id, 'q');

                    return;
                }
            }
        }
    }

    private long GetHoneyIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetHoneyIdForGoBackToParams(string data)
    {
        return Convert.ToInt64(data[13..]);
    }

    private long GetHoneyIdForDescription(string data)
    {
        return Convert.ToInt64(data[12..]);
    }

    private long GetHoneyIdForCount(string data)
    {
        return Convert.ToInt64(data[6..]);
    }

    private long GetHoneyIdForPrice(string data)
    {
        return Convert.ToInt64(data[6..]);
    }
}
