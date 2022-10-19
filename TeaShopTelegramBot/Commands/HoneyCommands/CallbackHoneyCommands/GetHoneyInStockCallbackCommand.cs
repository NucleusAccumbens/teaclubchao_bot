using Application.Herbs.Interfaces;
using Application.Honeys.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;
using TeaShopTelegramBot.Messages.HoneyMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class GetHoneyInStockCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAlertText =
    "🤷🏼 Здесь пока ничего нет...\n\n" +
    "Но это временно, не пропусти обновления ✨";

    private readonly string _englishAlertText =
        "🤷🏼 Nothing here yet...\n\n" +
        "But it's temporary, don't miss\nupdates ✨";

    private readonly string _hebrewAlertText =
        "🤷🏼 אין כאן עדיין...\n\n" +
         "אבל זה זמני, אל תפספסו עדכונים ✨";

    private readonly IGetHoneyQuery _getHoneyQuery;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly HoneyBuyerPageMessage _honeyBuyerPageMessage;

    private readonly NavigationMessage _navigationMessage;

    public GetHoneyInStockCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IGetHoneyQuery getHoneyQuery)
    {
        _getHoneyQuery = getHoneyQuery;
        _getUserLanguageQuery = getUserLanguageQuery;
        _honeyBuyerPageMessage = new(_getUserLanguageQuery);
        _navigationMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'Q';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {

        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            var honey = await _getHoneyQuery.GetAllHoneyAsync();

            await SendMessages(chatId, client, honey, callbackId);
        }
    }

    private async Task SendMessages(long chatId, ITelegramBotClient client, List<Honey> honey, string callbackId)
    {
        if (honey.Count > 0)
        {
            foreach (var h in honey)
            {
                if (h.InStock == true) await _honeyBuyerPageMessage
                        .GetMessage(chatId, client, h.Adapt<HoneyDto>());
            }

            await _navigationMessage.GetMessage(chatId, client);

            return;
        }

        await ShowAlert(chatId, client, callbackId);
    }

    private async Task ShowAlert(long chatId, ITelegramBotClient client, string callbackId)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService
                .ShowAllert(callbackId, client, _russianAlertText);

        if (language == Language.English) await MessageService
                .ShowAllert(callbackId, client, _englishAlertText);

        if (language == Language.Hebrew) await MessageService
                .ShowAllert(callbackId, client, _hebrewAlertText);
    }
}
