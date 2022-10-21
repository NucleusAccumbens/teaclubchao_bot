using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.OrderMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class DeliveryMethodCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAllertText = "Способ доставки изменён!";

    private readonly string _englishAllertText = "Delivery method has changed!";

    private readonly string _hebrewAllertText = "שיטת המשלוח השתנתה!";

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly DeliveryMethodMessage _deliveryMethodMessage;

    private readonly CartMessage _cartMessage;

    private readonly ContactsMessage _contactsMessage;

    public DeliveryMethodCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _deliveryMethodMessage = new(_getUserLanguageQuery);
        _cartMessage = new(_getUserLanguageQuery);
        _contactsMessage = new(_getUserLanguageQuery);
    }
    public override char CallbackDataCode => 'V';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            if (update.CallbackQuery.Data == "VDeliveryMethod")
            {
                if (orderDto != null)
                {
                    orderDto.ReceiptMethod = ReceiptMethods.Pickup;

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    await _deliveryMethodMessage.GetMessage(chatId, messageId, client);
                }

                return;
            }
            if (update.CallbackQuery.Data == "VDeliveryMethodGoBack")
            {
                if (orderDto != null) await GetPreviousStep(chatId, messageId, client, orderDto);
               
                return;
            }
            if (update.CallbackQuery.Data == "VPickup")
            {
                if (orderDto != null)
                {
                    orderDto.ReceiptMethod = ReceiptMethods.Pickup;

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    await ShowAllert(chatId, client, callbackId);

                    await _cartMessage.GetMessage(chatId, messageId, client, orderDto);
                }

                return;
            }
            if (update.CallbackQuery.Data == "VBoxberry")
            {
                if (orderDto != null)
                {
                    orderDto.ReceiptMethod = ReceiptMethods.Boxberry;

                    await GetNextStep(chatId, messageId, client, orderDto);
                }

                return;
            }
            if (update.CallbackQuery.Data == "VSDEK")
            {
                if (orderDto != null)
                {
                    orderDto.ReceiptMethod = ReceiptMethods.CDEK;

                    await GetNextStep(chatId, messageId, client, orderDto);
                }

                return;
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

    private async Task GetNextStep(long chatId, int messageId, ITelegramBotClient client, OrderDto orderDto)
    {
        _memoryCachService.SetMemoryCach(chatId, orderDto);

        _memoryCachService.SetMemoryCach(chatId, "userName");

        _memoryCachService.SetMemoryCach(chatId, messageId);

        await _contactsMessage.GetMessage(chatId, messageId, client);
    }

    private async Task GetPreviousStep(long chatId, int messageId, ITelegramBotClient client, OrderDto orderDto)
    {
        orderDto.ReceiptMethod = ReceiptMethods.Pickup;

        _memoryCachService.SetMemoryCach(chatId, orderDto);

        await _deliveryMethodMessage.GetMessage(chatId, messageId, client);
    }
}
