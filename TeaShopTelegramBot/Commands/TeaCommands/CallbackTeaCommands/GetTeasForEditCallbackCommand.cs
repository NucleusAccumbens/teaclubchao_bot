using Application.Teas.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class GetTeasForEditCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetTeaQuery _getTeaQuery;

    public GetTeasForEditCallbackCommand(IGetTeaQuery getTeaQuery)
    {
        _getTeaQuery = getTeaQuery;
    }

    public override char CallbackDataCode => 'h';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            List<Tea> teas = new();

            if (update.CallbackQuery.Data == "hRed") teas = await _getTeaQuery.GetAllRedTeaAsync();

            if (update.CallbackQuery.Data == "hGreen") teas = await _getTeaQuery.GetAllGreenTeaAsync();

            if (update.CallbackQuery.Data == "hWhite") teas = await _getTeaQuery.GetAllWhiteTeaAsync();

            if (update.CallbackQuery.Data == "hOloong") teas = await _getTeaQuery.GetAllOloongTeaAsync();

            if (update.CallbackQuery.Data == "hShuPuer") teas = await _getTeaQuery.GetAllShuPuerTeaAsync();

            if (update.CallbackQuery.Data == "hShenPuer") teas = await _getTeaQuery.GetAllShenPuerTeaAsync();

            if (update.CallbackQuery.Data == "hCraft") teas = await _getTeaQuery.GetAllCraftTeasAsync();

            foreach (var tea in teas)
            {
                await _productEditPageMessage.GetMessage(chatId, client, 
                    tea.Adapt<TeaDto>(), 'i');
            }

            return;
        }
    }
}
