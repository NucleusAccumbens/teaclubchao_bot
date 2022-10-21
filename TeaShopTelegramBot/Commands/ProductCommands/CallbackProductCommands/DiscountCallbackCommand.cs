using Application.Products.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.ProductCommands.CallbackProductCommands;

public class DiscountCallbackCommand : BaseCallbackCommand
{
    private readonly EditDiscountPageMessage _editDiscountPageMessage = new();

    private readonly DiscountMessage _discountMessage = new();

    private readonly IGetProductsQuery _getProductsQuery;

    private readonly IMemoryCachService _memoryCachService;

    public DiscountCallbackCommand(IGetProductsQuery getProductsQuery, IMemoryCachService memoryCachService)
    {
        _getProductsQuery = getProductsQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 's';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            if (update.CallbackQuery.Data.Contains("sDiscount"))
            {
                var product = await _getProductsQuery.GetProductAsync(GetProductIdForDiscount(data));

                if (product is Tea) _memoryCachService.SetMemoryCach(product.Adapt<TeaDto>());

                if (product is Herb) _memoryCachService.SetMemoryCach(product.Adapt<HerbDto>());

                if (product is Honey) _memoryCachService.SetMemoryCach(product.Adapt<HoneyDto>());

                _memoryCachService.SetMemoryCach("editDiscount", update);
                
                await _discountMessage
                    .GetMessage(chatId, messageId, client, GetProductIdForDiscount(data), 's');

                return;
            }
            if (update.CallbackQuery.Data.Contains("sBack"))
            {
                var product = await _getProductsQuery.GetProductAsync(GetProductIdForGoBack(data));

                if (product is Tea)
                {
                    await _editDiscountPageMessage
                        .GetMessage(chatId, messageId, client, product.Adapt<TeaDto>(), 's');

                    return;
                }
                if (product is Herb)
                {
                    await _editDiscountPageMessage
                        .GetMessage(chatId, messageId, client, product.Adapt<HerbDto>(), 's');

                    return;
                }
                if (product is Honey)
                {
                    await _editDiscountPageMessage
                        .GetMessage(chatId, messageId, client, product.Adapt<HoneyDto>(), 's');

                    return;
                }
            }
        }
    }

    private long GetProductIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }

    private long GetProductIdForDiscount(string data)
    {
        return Convert.ToInt64(data[9..]);
    }
}
