using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;
public class HerbRegressionCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;
    public override char CallbackDataCode => ',';

    public HerbRegressionCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
        {
            var herb = _memoryCachService.GetHerbDtoFromMemoryCach();

            if (update.CallbackQuery.Data == ",GoBackToSetName")
            {
                await GoBackToSetName(update, client, herb);
                return;
            }
            if (update.CallbackQuery.Data == ",GoBackToSetDescription")
            {
                await GoBackToSetDescription(update, client, herb);
                return;
            }
            if (update.CallbackQuery.Data == ",GoBackToSetWeight")
            {
                await GoBackToSetWeight(update, client, herb);
                return;
            }
            if (update.CallbackQuery.Data == ",GoBackToSetPrice")
            {
                await GoBackToSetPrice(update, client, herb);
                return;
            }
            if (update.CallbackQuery.Data == ",GoBackToSetCount")
            {
                await GoBackToSetCount(update, client, herb);
                return;
            }
        }
    }

    private async Task GoBackToSetCount(Update update, ITelegramBotClient client,
        HerbDto herbDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ",GoBackToSetPrice")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n" +
                $"<b>Вес сбора:</b> {HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n" +
                $"<b>Цена сбора:</b> {herbDto.Price}\n\n" +
                $"Осталось немного! Отправь количество цифрой в чат.",
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);

            _memoryCachService.SetMemoryCach("herbCount", update);
        }
    }

    private async Task GoBackToSetPrice(Update update, ITelegramBotClient client, 
        HerbDto herbDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
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
                $"<b>Вес сбора:</b> {HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n\n" +
                $"Отправь цену сбора сообщением в чат.",
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);

            _memoryCachService.SetMemoryCach("herbPrice", update);
        }
    }

    private async Task GoBackToSetWeight(Update update, ITelegramBotClient client, 
        HerbDto herbDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 50 г", callbackData: "F50"),
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 100 г", callbackData: "F100"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 150 г", callbackData: "F150"),
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 200 г", callbackData: "F200"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "⚖️ 250 г", callbackData: "F250"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ",GoBackToSetDescription")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n\n" +
                $"Нажми соответствующую кнопку, чтобы выбрать вес сбора.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach(String.Empty, update);
        }
    }

    private async Task GoBackToSetDescription(Update update, ITelegramBotClient client, 
        HerbDto herbDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ",GoBackToSetName")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n\n" +
                $"Теперь отправь сообщение с описанием сбора.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach("herbDescription", update);
        }
    }

    private async Task GoBackToSetName(Update update, ITelegramBotClient client, 
        HerbDto herbDto)
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
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n\n" +
                $"Отправь название сбора сообщением в этот чат.",
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);

            _memoryCachService.SetMemoryCach("herbName", update);
        }
    }
}
