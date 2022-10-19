using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralcallbackCommands;

public class LanguageCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAlertText = "✨ Готово! ✨\n" +
        "Выбор языка сохранён.\n\n" +
        "Чтобы изменить язык используйте команду /language";

    private readonly string _englishAlertText = "✨ Ready! ✨\n" +
        "The language selection is saved.\n\n" +
        "To change the language, use the" +
        "\n/language command";

    private readonly string _hebrevAlertText = "✨ מוכן! ✨\n" +
         "בחירת השפה נשמרת.\n\n" +
         "כדי לשנות את השפה, השתמש ב-" +
         "\n/language";

    private readonly IChangeLanguageCommand _changeLanguageCommand;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly MenuMessage _menuMessage; 

    public LanguageCallbackCommand(IChangeLanguageCommand changeLanguageCommand, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _changeLanguageCommand = changeLanguageCommand;
        _getUserLanguageQuery = getUserLanguageQuery;
        _menuMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => '~';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "~🇷🇺")
            {
                await _changeLanguageCommand.ChangeLanguageAsync(chatId, Language.Russian);

                await MessageService.ShowAllert(callbackId, client, _russianAlertText);

                await _menuMessage.GetMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "~🇬🇧")
            {
                await _changeLanguageCommand.ChangeLanguageAsync(chatId, Language.English);

                await MessageService.ShowAllert(callbackId, client, _englishAlertText);

                await _menuMessage.GetMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "~🇮🇱")
            {
                await _changeLanguageCommand.ChangeLanguageAsync(chatId, Language.Hebrew);

                await MessageService.ShowAllert(callbackId, client, _hebrevAlertText);

                await _menuMessage.GetMessage(chatId, messageId, client);
            }
        }
    }
}
