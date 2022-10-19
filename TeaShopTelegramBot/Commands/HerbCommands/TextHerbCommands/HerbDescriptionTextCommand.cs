using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;
using Telegram.Bot.Types;

namespace TeaShopTelegramBot.Commands.HerbCommands.TextHerbCommands;
public class HerbDescriptionTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public HerbDescriptionTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "herbDescription";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string messageText = update.Message.Text;

            try
            {
                var herb = _memoryCachService.GetHerbDtoFromMemoryCach();

                if (_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    return;
                }
                if (!_textCommandService.CheckMessageIsCommand(messageText))
                {
                    SetHerbDescriptionAndCommandState(_textCommandService
                        .CheckStringLessThan500(messageText), herb);

                    await DeleteMessage(update, client);

                    await EditMenuMessage(update, client, herb);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client, 
        HerbDto herbDto)
    {
        if (update.Message != null)
        {
            int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

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
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n\n" +
                $"Нажми соответствующую кнопку, чтобы выбрать вес сбора.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private async Task DeleteMessage(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            await client.DeleteMessageAsync(
                chatId: update.Message.Chat.Id,
                messageId: update.Message.MessageId);
        }
    }

    private void SetHerbDescriptionAndCommandState(string description, HerbDto herb)
    {
        herb.Description = description;
        _memoryCachService.SetMemoryCach("herbPrice", herb);
    }
}
