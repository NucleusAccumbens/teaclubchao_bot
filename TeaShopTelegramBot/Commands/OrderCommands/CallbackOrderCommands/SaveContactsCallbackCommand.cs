using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class SaveContactsCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAllertText = "Способ доставки изменён!";

    private readonly string _englishAllertText = "Shipping method changed!";

    private readonly string _hebrewAllertText = "שיטת המשלוח השתנתה!";

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly CartMessage _cartMessage;

    private readonly AddressMessage _addressMessage;

    public SaveContactsCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _cartMessage = new(_getUserLanguageQuery);
        _memoryCachService = memoryCachService;
        _addressMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'b';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (update.CallbackQuery.Data == "bSave")
            {
                _memoryCachService.SetMemoryCach(chatId, String.Empty);

                if (orderDto != null)
                {
                    await _cartMessage
                        .GetMessage(chatId, messageId, client, orderDto);

                    await ShowAllert(chatId, client, callbackId);
                }

                return;
            }
            if (update.CallbackQuery.Data == "bGoBack")
            {
                if (orderDto != null && orderDto.Contacts != null)
                {
                    orderDto.Contacts.Address = null;

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    _memoryCachService.SetMemoryCach(chatId, "address");

                    await _addressMessage
                        .GetMessage(chatId, messageId, client, orderDto.Contacts);
                }
            }
        }
    }

    private async Task ShowAllert(long chatId, ITelegramBotClient client, string callbackId)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService
                .ShowAllert(callbackId, client, _russianAllertText);

        if (language == Language.English) await MessageService
                .ShowAllert(callbackId, client, _englishAllertText);

        if (language == Language.Hebrew) await MessageService
                .ShowAllert(callbackId, client, _hebrewAllertText);
    }
}
