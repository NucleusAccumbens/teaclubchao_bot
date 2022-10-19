using Application.Herbs.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class GetHerbsForEditCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IGetHerbQuery _getHerbQuery;

    public GetHerbsForEditCallbackCommand(IGetHerbQuery getHerbQuery)
    {
        _getHerbQuery = getHerbQuery;
    }

    public override char CallbackDataCode => 'l';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            List<Herb> herbs = new();

            if (update.CallbackQuery.Data == "lAltai") herbs = await _getHerbQuery.GetAltaiHerbsAsync();

            if (update.CallbackQuery.Data == "lKarelia") herbs = await _getHerbQuery.GetKareliaHerbsAsync();

            if (update.CallbackQuery.Data == "lCaucasus") herbs = await _getHerbQuery.GetCaucasusHerbsAsync();

            if (update.CallbackQuery.Data == "lSiberia") herbs = await _getHerbQuery.GetSiberiaHerbsAsync();


            foreach (var herb in herbs)
            {
                await _productEditPageMessage.GetMessage(chatId, client,
                    herb.Adapt<HerbDto>(), 'm');
            }

            return;
        }
    }
}
