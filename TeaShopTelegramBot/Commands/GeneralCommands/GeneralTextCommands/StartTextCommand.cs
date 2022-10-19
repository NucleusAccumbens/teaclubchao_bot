using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.AdminMessages;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class StartTextCommand : BaseTextCommand
{
    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    private readonly ICreateTlgUserCommand _createTlgUserCommand;

    private readonly IСheckUserIsAdminQuery _checkUserIsAdminQuery;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly StartAdminMessage _startAdminMessage = new();

    private readonly LanguageMessage _languageMessage = new();

    private readonly ClientStartMessage _clientStartMessage = new();

    public StartTextCommand(ICreateTlgUserCommand tlgUserCommand, IСheckUserIsAdminQuery checkUserIsAdminQuery,
        ICheckUserIsInDbQuery checkUserIsInDbQuery, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _createTlgUserCommand = tlgUserCommand;
        _checkUserIsAdminQuery = checkUserIsAdminQuery;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override string Name => "/start";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            var chatId = update.Message.Chat.Id;

            if (!await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId))
            {
                await _createTlgUserCommand.CreateTlgUserAsync(update.Message.Chat);

                await _languageMessage.GetMessage(chatId, client);

                return;
            }

            bool? userIsAdmin = await _checkUserIsAdminQuery
                .CheckUserIsAdminAsync(chatId);

            if (userIsAdmin != null && userIsAdmin == true)
            {
                await _startAdminMessage.GetMessage(chatId, client);

                return;
            }
            if (userIsAdmin != null && userIsAdmin == false)
            {
                var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

                await _clientStartMessage.GetMessage(chatId, client, language);
            }
        }
    }
}

