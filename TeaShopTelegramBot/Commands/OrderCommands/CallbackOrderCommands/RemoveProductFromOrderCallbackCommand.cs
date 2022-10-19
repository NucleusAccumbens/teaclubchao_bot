using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.OrderMessages;

namespace TeaShopTelegramBot.Commands.OrderCommands.CallbackOrderCommands;

public class RemoveProductFromOrderCallbackCommand : BaseCallbackCommand
{
    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly RemoveProductMessage _removeProductMessage;

    private readonly CartMessage _cartMessage;

    public RemoveProductFromOrderCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
        _removeProductMessage = new(_getUserLanguageQuery);
        _cartMessage = new(_getUserLanguageQuery);
    }

    public override char CallbackDataCode => 'Z';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "ZRemoveProduct")
                {
                    if (orderDto != null && orderDto.Products != null)
                    {
                        foreach (var product in orderDto.Products)
                        {
                            await _removeProductMessage
                                .GetMessage(chatId, client, product);
                        }
                    }

                    return;
                }
                if (update.CallbackQuery.Data.Contains("ZRemove"))
                {
                    var productId = GetProductId(update.CallbackQuery.Data);

                    if (orderDto != null && orderDto.Products != null && orderDto.Products.Count > 0)
                    {
                        var productDto = orderDto.Products
                            .First(p => p.Id == productId);

                        orderDto.Products.Remove(productDto);

                        await MessageService.ShowAllert(callbackId, client,
                            await GetTextForAllert(chatId, productDto.Name));

                        _memoryCachService.SetMemoryCach(chatId, orderDto);

                        await _cartMessage.GetMessage(chatId, client,
                            _memoryCachService.GetOrderDtoFromMemoryCach(chatId));
                    }                   
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private long GetProductId(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private async Task<string> GetTextForAllert(long chatId, string? name)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) return $"Товар {name} удалён из корзины!";

        if (language == Language.English) return $"Item {name} has been removed from the cart!";

        return $"הפריט {name} הוסר מהסל!";
    }
}
