using Application.Products.Interfaces;
using Application.TlgUsers.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.GeneralCommands.GeneralTextCommands;

public class DiscountsTextCommand : BaseTextCommand
{
    private readonly EditDiscountPageMessage _editDiscountPageMessage = new();

    private readonly IGetProductsQuery _getProductsQuery;

    private readonly IСheckUserIsAdminQuery _checkUserIsAdminQuery;

    public DiscountsTextCommand(IGetProductsQuery getProductsQuery, IСheckUserIsAdminQuery сheckUserIsAdminQuery)
    {
        _getProductsQuery = getProductsQuery;
        _checkUserIsAdminQuery = сheckUserIsAdminQuery;
    }

    public override string Name => "/discounts";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            if (await _checkUserIsAdminQuery.CheckUserIsAdminAsync(chatId) == false) return;
            
            var products = await _getProductsQuery.GetDiscountProductsAsync();

            if (products != null && products.Count != 0)
            {
                foreach (var product in products)
                {
                    if (product is Tea)
                    {                       
                        await _editDiscountPageMessage
                        .GetMessage(chatId, client, product.Adapt<TeaDto>(), 's');
                    }
                    if (product is Herb)
                    {
                        await _editDiscountPageMessage
                        .GetMessage(chatId, client, product.Adapt<HerbDto>(), 's');
                    }
                    if (product is Honey)
                    {
                        await _editDiscountPageMessage
                        .GetMessage(chatId, client, product.Adapt<HoneyDto>(), 's');
                    }
                }
            }
        }
    }
}
