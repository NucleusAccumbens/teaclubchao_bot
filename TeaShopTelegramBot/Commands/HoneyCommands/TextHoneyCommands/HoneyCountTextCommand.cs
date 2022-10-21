using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;
public class HoneyCountTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public HoneyCountTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "honeyCount";

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

                    SetHoneyCount(messageText, honey);

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

    private void SetHoneyCount(string count, HoneyDto honey)
    {
        if (int.TryParse(count, out _) == true)
        {
            honey.Count = Convert.ToInt32(count);
            _memoryCachService.SetMemoryCach("honeyPhoto", honey);
            return;
        }
        if (int.TryParse(count, out _) == false)
        {
            throw new TryParseException();
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
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "<-GoBackToSetCount")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n" +
                $"<b>Вес мёда:</b> {HoneyEnumParser.GetHoneyWeightStringValue(honey.HoneyWeight)}\n" +
                $"<b>Цена мёда:</b> {honey.Price}\n" +
                $"<b>Количество мёда:</b> {honey.Count}\n\n" +
                $"Осталось только загрузить фотографию! Отправь фото мёда сообщением в этот чат.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private async Task GetAnswerForTryParseException(ITelegramBotClient client)
    {
        string callbackQueryId = _memoryCachService.GetCallbackQueryIdFromMemoryCatch();

        await client.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQueryId,
            text: $"‼️ Цена может быть только числом ‼️\n\n" +
            $"Попробуй установить цену снова.",
            showAlert: true);
    }
}
