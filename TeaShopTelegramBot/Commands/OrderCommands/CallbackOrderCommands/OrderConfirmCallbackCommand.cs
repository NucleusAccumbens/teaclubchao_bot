using Application.Orders.Interfaces;
using Application.Products.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.GeneralMessages;
using TeaShopTelegramBot.Messages.OrderMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class OrderConfirmCallbackCommand : BaseCallbackCommand
{
    private readonly string _russianAllertTextForDelivery = "Спасибо за заказ! " +
        "Трек номер накладной ждите в течение 2-3 дней.";

    private readonly string _russianAllertText = "Спасибо за заказ! " +
        "Администратор свяжется с вами в течение 2-3 дней.";

    private readonly string _russianUsernameIsNullAllertText = "Упс 😬\n\n" +
        "В вашем телеграм профиле отсутствует username, администратор не сможет связаться с вами 😢\n\n" +
        "Добавьте username в настройках профиля и подтвердите заказ 💚";

    private readonly string _englishAllertTextForDelivery = "Thanks for your order! " +
        "Please wait for the track number of the invoice within 2-3 days.";

    private readonly string _englishAllertText = "Thanks for your order! " +
        "The administrator will contact you within 2-3 days.";

    private readonly string _englishUsernameIsNullAllertText = "Oops 😬\n\n" +
         "There is no username in your telegram profile, the administrator will not be able to contact you 😢\n\n" +
         "Add username in profile settings and confirm order 💚";

    private readonly string _hebrewAllertTextForDelivery = "תודה על ההזמנה שלך!" +
        " אנא המתן למספר הרצועה של החשבונית תוך 2-3 ימים.";

    private readonly string _hebrewAllertText = "תודה על ההזמנה שלך!" +
        " המנהל ייצור איתך קשר תוך 2-3 ימים.";

    private readonly string _hebrewUsernameIsNullAllertText = "אופס 😬\n\n" +
         "אין שם משתמש בפרופיל הטלגרם שלך, המנהל לא יוכל ליצור איתך קשר 😢\n\n" +
         "הוסף שם משתמש בהגדרות הפרופיל ואשר את ההזמנה 💚";

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetAdminsQuery _getAdminsQuery;

    private readonly IChangeProductCountCommand _changeProductCountCommand;

    private readonly ConfirmedOrderAdminMessage _confirmedOrderAdminMessage = new();

    private readonly OrderConfirmMessage _confirmMessage;

    private readonly MenuMessage _menuMessage;

    public OrderConfirmCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService,
        IGetAdminsQuery getAdminsQuery, IChangeProductCountCommand changeProductCountCommand)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _confirmMessage = new(_getUserLanguageQuery);
        _menuMessage = new(_getUserLanguageQuery);
        _getAdminsQuery = getAdminsQuery;
        _changeProductCountCommand = changeProductCountCommand;
    }

    public override char CallbackDataCode => 'X';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string? username = update.CallbackQuery.Message.Chat.Username;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "XOrderConfirm")
                {
                    if (orderDto != null)
                    {
                        await _confirmMessage
                            .GetMessage(chatId, messageId, client, orderDto);
                    }

                    return;
                }
                if (update.CallbackQuery.Data == "XConfirm")
                {
                    if (username == null)
                    {
                        await ShowAllertForUsernameIsNull(chatId, client, callbackId);

                        return;
                    }
                    
                    if (orderDto != null)
                    {
                        await ChangeProductCount(orderDto);
                        
                        await SendMessageToAdmins(username, client, orderDto);

                        if (orderDto.ReceiptMethod == ReceiptMethods.CDEK ||
                        orderDto.ReceiptMethod == ReceiptMethods.Boxberry) await
                                ShowAllertForDelivery(chatId, client, callbackId);

                        if (orderDto.ReceiptMethod == ReceiptMethods.Pickup) await
                                ShowAllert(chatId, client, callbackId);

                        await _menuMessage
                            .GetMessage(chatId, messageId, client);

                    }
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task ChangeProductCount(OrderDto orderDto)
    {
        if (orderDto.Products != null)
        {
            foreach (var product in orderDto.Products)
            {
                await _changeProductCountCommand
                    .SubtractOneFromCountAsync(product.Id);
            }
        }
    }

    private async Task ShowAllertForDelivery(long chatId, ITelegramBotClient client, string callbackId)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService
                .ShowAllert(callbackId, client, _russianAllertTextForDelivery);

        if (language == Language.English) await MessageService
                .ShowAllert(callbackId, client, _englishAllertTextForDelivery);

        if (language == Language.Hebrew) await MessageService
                .ShowAllert(callbackId, client, _hebrewAllertTextForDelivery);
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

    private async Task ShowAllertForUsernameIsNull(long chatId, ITelegramBotClient client, string callbackId)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService
                .ShowAllert(callbackId, client, _russianUsernameIsNullAllertText);

        if (language == Language.English) await MessageService
                .ShowAllert(callbackId, client, _englishUsernameIsNullAllertText);

        if (language == Language.Hebrew) await MessageService
                .ShowAllert(callbackId, client, _hebrewUsernameIsNullAllertText);
    }

    private async Task SendMessageToAdmins(string? username, ITelegramBotClient client, OrderDto orderDto)
    {
        var admins = await _getAdminsQuery.GetAdminsAsync();

        foreach (var admin in admins)
        {
            await _confirmedOrderAdminMessage
                .GetMessage(admin.ChatId, username, client, orderDto);
        }
    }
}
