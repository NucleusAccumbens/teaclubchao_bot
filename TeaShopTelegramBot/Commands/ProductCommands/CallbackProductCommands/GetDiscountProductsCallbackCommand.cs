using Application.Products.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.HerbMessages;
using TeaShopTelegramBot.Messages.HoneyMessages;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.ProductCommands.CallbackProductCommands;

public class GetDiscountProductsCallbackCommand : BaseCallbackCommand
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

    private readonly TeaBuyerPageMessage _teaBuyerPageMessage;

    private readonly HerbBuyerPageMessage _herbBuyerPageMessage;

    private readonly HoneyBuyerPageMessage _honeyBuyerPageMessage;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IGetProductsQuery _getProductsQuery;

    public GetDiscountProductsCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IGetProductsQuery getProductsQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _getProductsQuery = getProductsQuery;
        _teaBuyerPageMessage = new(_getUserLanguageQuery);
        _herbBuyerPageMessage = new(_getUserLanguageQuery);
        _honeyBuyerPageMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => '№';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            var discountProducts = await _getProductsQuery
                .GetDiscountProductsAsync();

            if (discountProducts != null && discountProducts.Count > 0)
            {
                foreach (var product in discountProducts)
                {
                    if (product is Tea)
                    {
                        await _teaBuyerPageMessage
                            .GetMessage(chatId, client, product.Adapt<TeaDto>());
                    }
                    if (product is Herb)
                    {
                        await _herbBuyerPageMessage
                            .GetMessage(chatId, client, product.Adapt<HerbDto>());
                    }
                    if (product is Honey)
                    {
                        await _honeyBuyerPageMessage
                            .GetMessage(chatId, client, product.Adapt<HoneyDto>());
                    }
                }

                return;
            }
            if (discountProducts == null || discountProducts.Count == 0) 
                await ShowAllertForDiscountProductsIsNull(chatId, callbackId, client);
        }
    }

    private async Task ShowAllertForDiscountProductsIsNull(long chatId, string callbackId, ITelegramBotClient client)
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
