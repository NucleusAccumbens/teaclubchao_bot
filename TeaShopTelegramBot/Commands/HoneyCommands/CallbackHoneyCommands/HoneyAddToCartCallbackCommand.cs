using Application.Herbs.Interfaces;
using Application.Honeys.Interfaces;
using Application.TlgUsers.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class HoneyAddToCartCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IGetHoneyQuery _getHoneyQuery;

    public HoneyAddToCartCallbackCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery,
        IGetHoneyQuery getHoneyQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
        _getHoneyQuery = getHoneyQuery;
    }

    public override char CallbackDataCode => 'T';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            string data = update.CallbackQuery.Data;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            var honey = await _getHoneyQuery.GetHoneyAsync(GetHoneyId(data));

            if (orderDto == null)
            {
                orderDto = new() { UserChatId = chatId };

                if (honey != null && honey.Name != null)
                {
                    orderDto.Products.Add(honey.Adapt<HoneyDto>());

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    await MessageService.ShowAllert(callbackId, client,
                        await GetTextForAllert(chatId, honey.Name));
                }

                return;
            }

            if (honey != null && honey.Name != null)
            {
                orderDto.Products.Add(honey.Adapt<HoneyDto>());

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                await MessageService.ShowAllert(callbackId, client,
                    await GetTextForAllert(chatId, honey.Name));
            }
        }
    }

    private async Task<string> GetTextForAllert(long chatId, string name)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) return $"Мёд {name} добавлен в корзину!";

        if (language == Language.English) return $"Honey {name} added to cart!";

        return $"מותק {name} נוסף לעגלה!";
    }

    private long GetHoneyId(string data)
    {
        return Convert.ToInt64(data[15..]);
    }
}
