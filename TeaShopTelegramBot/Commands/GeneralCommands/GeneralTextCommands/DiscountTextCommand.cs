using Application.Products.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.GeneralMessages;
using TeaShopTelegramBot.Messages.HerbMessages;
using TeaShopTelegramBot.Messages.HoneyMessages;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class DiscountTextCommand : BaseTextCommand
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

    public DiscountTextCommand(IGetUserLanguageQuery getUserLanguageQuery, IGetProductsQuery getProductsQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _getProductsQuery = getProductsQuery;        
        _teaBuyerPageMessage = new(_getUserLanguageQuery);
        _herbBuyerPageMessage = new(_getUserLanguageQuery);
        _honeyBuyerPageMessage = new(_getUserLanguageQuery);
    }

    public override string Name => "/discount";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            var chatId = update.Message.Chat.Id;

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
            }
            if (discountProducts == null || discountProducts.Count == 0) await SendMessageForDiscountProductsIsNull(chatId, client);
        }
    }

    private async Task SendMessageForDiscountProductsIsNull(long chatId, ITelegramBotClient client)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) await MessageService
                .SendMessage(chatId, client, _russianAlertText, null);

        if (language == Language.English) await MessageService
                .SendMessage(chatId, client, _englishAlertText, null);

        if (language == Language.Hebrew) await MessageService
                .SendMessage(chatId, client, _hebrewAlertText, null);
    }
}
