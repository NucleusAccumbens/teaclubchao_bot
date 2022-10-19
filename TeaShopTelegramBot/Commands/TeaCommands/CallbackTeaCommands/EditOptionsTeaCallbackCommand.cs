using Application.Teas.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class EditOptionsTeaCallbackCommand : BaseCallbackCommand
{
    private readonly RemoveProductMessage _removeProductMessage = new();

    private readonly ProductEditOptionsMessage _productEditOptionsMessage = new();

    private readonly DiscountMessage _discountMessage = new(); 

    private readonly IGetTeaQuery _getTeaQuery;

    private readonly IMemoryCachService _memoryCachService;

    public EditOptionsTeaCallbackCommand(IGetTeaQuery getTeaQuery, IMemoryCachService memoryCachService)
    {
        _getTeaQuery = getTeaQuery;
        _memoryCachService = memoryCachService;
    }
    
    public override char CallbackDataCode => 'i';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("iEdit"))
            {
                await _productEditOptionsMessage.GetMessage(chatId, messageId, client, GetTeaIdForEdit(data), 'j');

                return;
            }
            if (update.CallbackQuery.Data.Contains("iRemove"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForRemove(data));

                await _removeProductMessage.GetMessage(chatId, messageId, client, tea.Adapt<TeaDto>(), 'k');

                return;
            }
            if (update.CallbackQuery.Data.Contains("iDiscount"))
            {
                var tea = await _getTeaQuery.GetTeaAsync(GetTeaIdForDiscount(data));
                
                _memoryCachService.SetMemoryCach(tea.Adapt<TeaDto>());
                
                _memoryCachService.SetMemoryCach("teaDiscount", update);
                
                await _discountMessage.GetMessage(chatId, messageId, client, tea.Id, 'j');
            }
        }
    }

    private long GetTeaIdForEdit(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetTeaIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetTeaIdForDiscount(string data)
    {
        return Convert.ToInt64(data[9..]);
    }
}
