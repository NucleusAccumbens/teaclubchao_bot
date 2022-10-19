using Application.Herbs.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class EditingHerbParametrsCallbackCommand : BaseCallbackCommand
{
    private readonly EditingProductParametersMessage _editingProductParametersMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetHerbQuery _getHerbQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditingHerbParametrsCallbackCommand(IGetHerbQuery getHerbQuery, IMemoryCachService memoryCachService)
    {
        _getHerbQuery = getHerbQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'n';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("nBackToParams"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client,
                        GetHerbIdForGoBackToParams(data), 'n');

                return;
            }
            if (update.CallbackQuery.Data.Contains("nBack"))
            {
                _memoryCachService.SetMemoryCach(String.Empty, update);
                
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForGoBack(data));

                if (herb != null)
                {
                    await _productEditPageMessage.GetMessage(chatId, messageId, client,
                        herb.Adapt<HerbDto>(), 'm');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("nDescription"))
            {
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForDescription(data));

                if (herb != null)
                {
                    _memoryCachService.SetMemoryCach(herb.Adapt<HerbDto>());

                    _memoryCachService.SetMemoryCach("editHerbDescription", update);

                    await _editingProductParametersMessage.GetMessageToEditDescription(chatId, messageId,
                        client, herb.Id, 'n');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("nCount"))
            {
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForCount(data));

                if (herb != null)
                {
                    _memoryCachService.SetMemoryCach(herb.Adapt<HerbDto>());

                    _memoryCachService.SetMemoryCach("editHerbCount", update);

                    await _editingProductParametersMessage.GetMessageToEditCount(chatId, messageId,
                        client, herb.Id, 'n');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("nPrice"))
            {
                var herb = await _getHerbQuery.GetHerbAsync(GetHerbIdForPrice(data));

                if (herb != null)
                {
                    _memoryCachService.SetMemoryCach(herb.Adapt<HerbDto>());

                    _memoryCachService.SetMemoryCach("editHerbPrice", update);

                    await _editingProductParametersMessage.GetMessageToEditPrice(chatId, messageId,
                        client, herb.Id, 'n');

                    return;
                }
            }
        }
    }

    private long GetHerbIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetHerbIdForGoBackToParams(string data)
    {
        return Convert.ToInt64(data[13..]);
    }

    private long GetHerbIdForDescription(string data)
    {
        return Convert.ToInt64(data[12..]);
    }

    private long GetHerbIdForCount(string data)
    {
        return Convert.ToInt64(data[6..]);
    }

    private long GetHerbIdForPrice(string data)
    {
        return Convert.ToInt64(data[6..]);
    }
}
