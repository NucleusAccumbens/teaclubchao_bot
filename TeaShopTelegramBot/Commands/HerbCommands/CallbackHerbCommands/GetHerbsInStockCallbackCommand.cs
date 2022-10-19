using Application.Herbs.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;
using TeaShopTelegramBot.Messages.HerbMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class GetHerbsInStockCallbackCommand : BaseCallbackCommand
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

    private readonly IGetHerbQuery _getHerbsQuery;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly HerbBuyerPageMessage _herbBuyerPageMessage;

    private readonly NavigationMessage _navigationMessage;

    public GetHerbsInStockCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IGetHerbQuery getHerbsQuery)
    {
        _getHerbsQuery = getHerbsQuery;
        _getUserLanguageQuery = getUserLanguageQuery;
        _herbBuyerPageMessage = new(_getUserLanguageQuery);
        _navigationMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'O';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "OAltai")
            {
                var herbs = await _getHerbsQuery.GetAltaiHerbsAsync();

                await SendMessages(chatId, client, herbs, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "OKarelia")
            {
                var herbs = await _getHerbsQuery.GetKareliaHerbsAsync();

                await SendMessages(chatId, client, herbs, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "OCaucasus")
            {
                var herbs = await _getHerbsQuery.GetCaucasusHerbsAsync();

                await SendMessages(chatId, client, herbs, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "OSiberia")
            {
                var herbs = await _getHerbsQuery.GetSiberiaHerbsAsync();

                await SendMessages(chatId, client, herbs, callbackId);
            }
        }
    }

    private async Task SendMessages(long chatId, ITelegramBotClient client, List<Herb> herbs, string callbackId)
    {
        if (herbs.Count > 0)
        {
            foreach (var herb in herbs)
            {
                if (herb.InStock == true) await _herbBuyerPageMessage
                        .GetMessage(chatId, client, herb.Adapt<HerbDto>());
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
