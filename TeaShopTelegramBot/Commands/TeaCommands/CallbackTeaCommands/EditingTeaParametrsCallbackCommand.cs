using Application.Teas.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class EditingTeaParametrsCallbackCommand : BaseCallbackCommand
{
    private readonly EditingProductParametersMessage _editingProductParametersMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetTeaQuery _getTeaQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditingTeaParametrsCallbackCommand(IGetTeaQuery getTeaQuery, IMemoryCachService memoryCachService)
    {
        _getTeaQuery = getTeaQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'j';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if(update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("jBackToParams"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client,
                        GetTeaIdForGoBackToParams(data), 'j');

                return;
            }
            if (update.CallbackQuery.Data.Contains("jBack"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForGoBack(data));

                _memoryCachService.SetMemoryCach(String.Empty, update);

                if (tea != null)
                {
                    await _productEditPageMessage.GetMessage(chatId, messageId, client,
                        tea.Adapt<TeaDto>(), 'i');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("jDescription"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForDescription(data));

                if (tea != null)
                {
                    _memoryCachService.SetMemoryCach(tea.Adapt<TeaDto>());

                    _memoryCachService.SetMemoryCach("editTeaDescription", update);
                
                    await _editingProductParametersMessage.GetMessageToEditDescription(chatId, messageId, 
                        client, tea.Id, 'j');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("jCount"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForCount(data));

                if (tea != null)
                {
                    _memoryCachService.SetMemoryCach(tea.Adapt<TeaDto>());

                    _memoryCachService.SetMemoryCach("editTeaCount", update);

                    await _editingProductParametersMessage.GetMessageToEditCount(chatId, messageId,
                        client, tea.Id, 'j');

                    return;
                }
            }
            if (update.CallbackQuery.Data.Contains("jPrice"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForPrice(data));

                if (tea != null)
                {
                    _memoryCachService.SetMemoryCach(tea.Adapt<TeaDto>());

                    _memoryCachService.SetMemoryCach("editTeaPrice", update);

                    await _editingProductParametersMessage.GetMessageToEditPrice(chatId, messageId,
                        client, tea.Id, 'j');

                    return;
                }
            }
        }
    }

    private long GetTeaIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetTeaIdForGoBackToParams(string data)
    {
        return Convert.ToInt64(data[13..]);
    }

    private long GetTeaIdForDescription(string data)
    {
        return Convert.ToInt64(data[12..]);
    }

    private long GetTeaIdForCount(string data)
    {
        return Convert.ToInt64(data[6..]);
    }

    private long GetTeaIdForPrice(string data)
    {
        return Convert.ToInt64(data[6..]);
    }
}
