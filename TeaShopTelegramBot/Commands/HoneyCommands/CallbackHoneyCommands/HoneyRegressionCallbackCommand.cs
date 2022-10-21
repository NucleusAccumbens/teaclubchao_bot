using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;
public class HoneyRegressionCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;
    public override char CallbackDataCode => '<';

    public HoneyRegressionCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null)
        {
            var honey = _memoryCachService.GetHoneyDtoFromMemoryCach();

            if (update.CallbackQuery.Data == "<-GoBackToSetWeight")
            {
                await GoBackToSetHoneyWeight(update, client, honey);
                return;
            }
            if (update.CallbackQuery.Data == "<-GoBackToSetPrice")
            {
                await GoBackToSetHoneyPrice(update, client, honey);
                return;
            }
            if (update.CallbackQuery.Data == "<-GoBackToSetCount")
            {
                await GoBackToSetHoneyCount(update, client, honey);
                return;
            }
        }
    }

    private async Task GoBackToSetHoneyWeight(Update update, ITelegramBotClient client, 
        HoneyDto honey)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            var messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 350 г", callbackData: "DThreeHundredFifty"),
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 950 г", callbackData: "DNineHundredFifty")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "DBackToSetDescription")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n\n" +
                $"Теперь укажи вес мёда.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private async Task GoBackToSetHoneyPrice(Update update, ITelegramBotClient client, 
        HoneyDto honey)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            var messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

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

            _memoryCachService.SetMemoryCach("honeyPrice", honey);
        }
    }

    private async Task GoBackToSetHoneyCount(Update update, ITelegramBotClient client,
        HoneyDto honey)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            var messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "<-GoBackToSetPrice")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n" +
                $"<b>Вес мёда:</b> {HoneyEnumParser.GetHoneyWeightStringValue(honey.HoneyWeight)}\n" +
                $"<b>Цена мёда:</b> {honey.Price}\n\n" +
                $"Почти готово! Укажи количество единиц товара - отправь цифру в чат.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach("honeyCount", honey);
        }
    }

}
