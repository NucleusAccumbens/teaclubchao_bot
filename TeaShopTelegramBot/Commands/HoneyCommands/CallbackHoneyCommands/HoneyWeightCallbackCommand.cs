using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;
public class HoneyWeightCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;
    public override char CallbackDataCode => 'D';

    public HoneyWeightCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null)
        {
            var honey = _memoryCachService.GetHoneyDtoFromMemoryCach();

            if (update.CallbackQuery.Data == "DThreeHundredFifty")
            {
                SetHoneyWeight(HoneyWeight.ThreeHundredFifty, honey);
                await EditMenuMessage(update, client, honey);
                return;
            }
            if (update.CallbackQuery.Data == "DNineHundredFifty")
            {
                SetHoneyWeight(HoneyWeight.NineHundredFifty, honey);
                await EditMenuMessage(update, client,  honey);
                return;
            }
            if (update.CallbackQuery.Data == "DBackToSetDescription")
            {
                await GoBackToSetDescription(update, client, honey);
                return;
            }
        }
    }

    private void SetHoneyWeight(HoneyWeight honeyWeight, HoneyDto honey)
    {
        honey.HoneyWeight = honeyWeight;

        _memoryCachService.SetMemoryCach("honeyPrice", honey);
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client,
        HoneyDto honey)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "<-GoBackToSetWeight")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n" +
                $"<b>Вес мёда:</b> {HoneyEnumParser.GetHoneyWeightStringValue(honey.HoneyWeight)}\n\n" +
                $"Отправь цену мёда сообщением в этот чат.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private async Task GoBackToSetDescription(Update update, ITelegramBotClient client,
        HoneyDto honey)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "C")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n\n" +
                $"Отправь описание мёда сообщением в этот чат или вернись к предыдущему действию.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach("honeyDescription", update, honey);
        }
    }
}
