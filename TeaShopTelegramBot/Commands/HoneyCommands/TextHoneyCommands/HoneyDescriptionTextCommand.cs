using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;

public class HoneyDescriptionTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public HoneyDescriptionTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "honeyDescription";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string messageText = update.Message.Text;

            if (_textCommandService.CheckMessageIsCommand(messageText))
            {
                await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                return;
            }
            if (!_textCommandService.CheckMessageIsCommand(messageText))
            {
                try
                {
                    var honey = _memoryCachService.GetHoneyDtoFromMemoryCach();

                    SetHoneyDescription(messageText, honey);

                    await DeleteMessage(update, client);

                    await EditMenuMessage(update, client, honey);
                }
                catch (MemoryCachException ex)
                {
                    await ex.SendExceptionMessage(chatId, client);
                }
            }
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

    private async Task EditMenuMessage(Update update, ITelegramBotClient client, 
        HoneyDto honey)
    {
        if (update.Message != null)
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
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {update.Message.Text}\n\n" +
                $"Теперь укажи вес мёда.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private void SetHoneyDescription(string description, HoneyDto honey)
    {
        honey.Description = description;
        _memoryCachService.SetMemoryCach(honey);
    }
}
