using Application.Teas.Interfaces;
using Application.TlgUsers.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaAddToCartCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IGetTeaQuery _getTeaQuery;

    public TeaAddToCartCallbackCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery,
        IGetTeaQuery getTeaQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
        _getTeaQuery = getTeaQuery;
    }

    public override char CallbackDataCode => 'R';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            string data = update.CallbackQuery.Data;

            var orderDto = _memoryCachService.GetOrderDtoFromMemoryCach(chatId);

            var tea = await _getTeaQuery.GetTeaAsync(GetTeaId(data));

            if (orderDto == null)
            {
                orderDto = new() { UserChatId = chatId };

                if (tea != null && tea.Name != null)
                {
                    orderDto.Products.Add(tea.Adapt<TeaDto>());

                    _memoryCachService.SetMemoryCach(chatId, orderDto);

                    await MessageService.ShowAllert(callbackId, client,
                        await GetTextForAllert(chatId, tea.Name));
                }

                return;
            }

            if (tea != null && tea.Name != null)
            {
                orderDto.Products.Add(tea.Adapt<TeaDto>());

                _memoryCachService.SetMemoryCach(chatId, orderDto);

                await MessageService.ShowAllert(callbackId, client,
                    await GetTextForAllert(chatId, tea.Name));
            }
        }
    }

    private async Task<string> GetTextForAllert(long chatId, string name)
    {
        var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

        if (language == Language.Russian) return $"Чай {name} добавлен в корзину!";

        if (language == Language.English) return $"Tea {name} added to cart!";

        return $"התה {name} נוסף לעגלת הקניות!";
    }

    private long GetTeaId(string data)
    {
        return Convert.ToInt64(data[13..]);
    }
}
