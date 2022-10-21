using Application.Teas.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class GetTeasInStockCallbackCommand : BaseCallbackCommand
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

    private readonly IGetTeaQuery _getTeasCommand;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly TeaBuyerPageMessage _teaBuyerPageMessage;

    private readonly NavigationMessage _navigationMessage;

    public GetTeasInStockCallbackCommand(IGetTeaQuery getTeasCommand, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getTeasCommand = getTeasCommand;
        _getUserLanguageQuery = getUserLanguageQuery;
        _teaBuyerPageMessage = new(_getUserLanguageQuery);
        _navigationMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'N';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "NRed")
            {
                var teas = await _getTeasCommand.GetAllRedTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NGreen")
            {
                var teas = await _getTeasCommand.GetAllGreenTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NWhite")
            {
                var teas = await _getTeasCommand.GetAllWhiteTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NOolong")
            {
                var teas = await _getTeasCommand.GetAllOloongTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NShuPuer")
            {
                var teas = await _getTeasCommand.GetAllShuPuerTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NShenPuer")
            {
                var teas = await _getTeasCommand.GetAllShenPuerTeaAsync();

                await SendMessages(chatId, client, teas, callbackId);

                return;
            }
            if (update.CallbackQuery.Data == "NCraft")
            {
                var teas = await _getTeasCommand.GetAllCraftTeasAsync();

                await SendMessages(chatId, client, teas, callbackId);
            }
        }
    }

    private async Task SendMessages(long chatId, ITelegramBotClient client, List<Tea> teas, string callbackId)
    {
        if (teas.Count > 0)
        {
            foreach (var tea in teas)
            {
                if (tea.InStock == true) await _teaBuyerPageMessage
                        .GetMessage(chatId, client, tea.Adapt<TeaDto>());
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
