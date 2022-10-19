using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;
public class HerbWeightCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public override char CallbackDataCode => 'F';

    public HerbWeightCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            try
            {
                if (update.CallbackQuery.Data == "F50")
                {
                    await EditMenuMessage(update, client, HerbsWeight.Fifty);

                    return;
                }
                if (update.CallbackQuery.Data == "F100")
                {
                    await EditMenuMessage(update, client, HerbsWeight.OneHundred);

                    return;
                }
                if (update.CallbackQuery.Data == "F150")
                {
                    await EditMenuMessage(update, client, HerbsWeight.OneHundredFifty);

                    return;
                }
                if (update.CallbackQuery.Data == "F200")
                {
                    await EditMenuMessage(update, client, HerbsWeight.TwoHundred);

                    return;
                }
                if (update.CallbackQuery.Data == "F250")
                {
                    await EditMenuMessage(update, client, HerbsWeight.TwoHundredFifty);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }           
        }
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client, 
        HerbsWeight weight)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            var herbDto = _memoryCachService.GetHerbDtoFromMemoryCach();

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ",GoBackToSetWeight")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n" +
                $"<b>Вес сбора:</b> {HerbEnumParser.GetHerbWeightStringValue(weight)}\n\n" +
                $"Отправь цену сбора сообщением в чат.",
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);

            SetHerbRegionAndCommandState(weight, herbDto, update);
        }
    }

    private void SetHerbRegionAndCommandState(HerbsWeight weight, HerbDto herbDto, Update update)
    {
        herbDto.Weight = weight;

        _memoryCachService.SetMemoryCach("herbPrice", update);

        _memoryCachService.SetMemoryCach(herbDto);
    }
}
