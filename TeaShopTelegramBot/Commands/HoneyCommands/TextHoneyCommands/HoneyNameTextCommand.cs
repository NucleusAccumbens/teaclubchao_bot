using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Models;
using Telegram.Bot.Types;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;

public class HoneyNameTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;
    public override string Name => "honeyName";

    public HoneyNameTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

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
                await DeleteMessage(update, client);

                await EditMenuMessage(update, client);

                SetHoneyNameAndCommandState(update.Message.Text);
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

    private async Task EditMenuMessage(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "C")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {update.Message.Text}\n\n" +
                $"Отправь описание мёда сообщением в этот чат или вернись к предыдущему действию.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private void SetHoneyNameAndCommandState(string name)
    {
        var honey = new HoneyDto()
        {
            Name = name
        };

        _memoryCachService.SetMemoryCach("honeyDescription", honey);
    }
}

