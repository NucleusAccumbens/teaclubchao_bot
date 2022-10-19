using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;
public class HerbRegionCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public override char CallbackDataCode => 'I';

    public HerbRegionCallbackCommand(IMemoryCachService memoryCachService)
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
                if (update.CallbackQuery.Data == "IAltai")
                {
                    await EditMenuMessage(update, client, HerbsRegion.Altai);

                    return;
                }
                if (update.CallbackQuery.Data == "ICaucasus")
                {
                    await EditMenuMessage(update, client, HerbsRegion.Caucasus);

                    return;
                }
                if (update.CallbackQuery.Data == "IKarelia")
                {
                    await EditMenuMessage(update, client, HerbsRegion.Karelia);

                    return;
                }
                if (update.CallbackQuery.Data == "ISiberia")
                {
                    await EditMenuMessage(update, client, HerbsRegion.Siberia);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client,
        HerbsRegion region)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "B")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(region)}\n\n" +
                $"Отправь название сбора сообщением в этот чат.",
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);

            SetHerbRegionAndCommandState(region, update);
        }
    }

    private void SetHerbRegionAndCommandState(HerbsRegion herbsRegion, Update update)
    {
        var herb = new HerbDto()
        {
            Region = herbsRegion
        };

        _memoryCachService.SetMemoryCach("herbName", update);

        _memoryCachService.SetMemoryCach(herb);
    }
}
