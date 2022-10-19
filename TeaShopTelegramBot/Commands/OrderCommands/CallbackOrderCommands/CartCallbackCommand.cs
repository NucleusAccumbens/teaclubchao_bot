using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class CartCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAlertText =
        "🤷🏻‍♀️ Корзина пуста...Положи в неё что-нибудь";

    private readonly string _englishAlertText =
        "🤷🏻‍♀️ Cart is empty...Put something in it";

    private readonly string _hebrewAlertText =
        "🤷🏻‍♀️ העגלה ריקה... נא לשים בה משהו";


    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly CartMessage _cartMessage;

    public CartCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _cartMessage = new(_getUserLanguageQuery);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => '/';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (orderDto != null)
            {
                await _cartMessage.GetMessage(chatId, messageId, client, orderDto);

                return;
            }

            await ShowAlert(chatId, client, callbackId);
        }
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
