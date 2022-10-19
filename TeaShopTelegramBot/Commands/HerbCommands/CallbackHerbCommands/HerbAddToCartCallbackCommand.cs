using Application.Herbs.Interfaces;
using Application.Teas.Interfaces;
using Application.Teas.Queries;
using Application.TlgUsers.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class HerbAddToCartCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IGetHerbQuery _getHerbQuery;

    public HerbAddToCartCallbackCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery,
        IGetHerbQuery getHerbQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
        _getHerbQuery = getHerbQuery;
    }

    public override char CallbackDataCode => 'S';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            string data = update.CallbackQuery.Data;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            var herb = await _getHerbQuery.GetHerbAsync(GetHerbId(data));

            if (orderDto == null)
            {
                orderDto = new() { UserChatId = chatId };

                if (herb != null && herb.Name != null)
                {
                    orderDto.Products.Add(herb.Adapt<HerbDto>());

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    await MessageService.ShowAllert(callbackId, client,
                        await GetTextForAllert(chatId, herb.Name));
                }

                return;
            }

            if (herb != null && herb.Name != null)
            {
                orderDto.Products.Add(herb.Adapt<HerbDto>());

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                await MessageService.ShowAllert(callbackId, client,
                    await GetTextForAllert(chatId, herb.Name));
            }
        }
    }

    private async Task<string> GetTextForAllert(long chatId, string name)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) return $"Сбор {name} добавлен в корзину!";

        if (language == Language.English) return $"Herb {name} added to cart!";

        return $"עשב {name} נוסף לעגלת הקניות!";
    }

    private long GetHerbId(string data)
    {
        return Convert.ToInt64(data[14..]);
    }
}
