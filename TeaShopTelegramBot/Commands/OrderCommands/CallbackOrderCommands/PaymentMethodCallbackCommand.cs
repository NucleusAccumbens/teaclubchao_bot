using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class PaymentMethodCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAllertText = "Способ оплаты изменён!";

    private readonly string _englishAllertText = "Payment method has been changed!";

    private readonly string _hebrewAllertText = "שיטת התשלום השתנתה!";

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly PaymentMethodMessage _paymentMethodMessage;

    private readonly CartMessage _cartMessage;

    public PaymentMethodCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _paymentMethodMessage = new(_getUserLanguageQuery);
        _cartMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'U';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "UPaymentMethod")
                {
                    await _paymentMethodMessage.GetMessage(chatId, messageId, client);

                    return;
                }
                if (update.CallbackQuery.Data == "URemittance")
                {
                    if (orderDto != null)
                    {
                        orderDto.PaymentMethod = PaymentMethods.Remittance;

                        _memoryCachService.SetMemoryCach(chatId, orderDto);

                        await ShowAllert(chatId, client, callbackId);

                        await _cartMessage.GetMessage(chatId, messageId, client, orderDto);
                    }

                    return;
                }
                if (update.CallbackQuery.Data == "UCash")
                {
                    if (orderDto != null)
                    {
                        orderDto.PaymentMethod = PaymentMethods.Cash;

                        _memoryCachService.SetMemoryCach(chatId, orderDto);

                        await ShowAllert(chatId, client, callbackId);

                        await _cartMessage.GetMessage(chatId, messageId, client, orderDto);
                    }

                    return;
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
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
