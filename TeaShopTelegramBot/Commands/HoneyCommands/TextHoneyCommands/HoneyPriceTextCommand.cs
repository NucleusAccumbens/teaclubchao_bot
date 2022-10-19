using Domain.Enums;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;
public class HoneyPriceTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public HoneyPriceTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "honeyPrice";

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

                    await DeleteMessage(update, client);

                    SetHoneyPrice(messageText, honey);

                    await EditMenuMessage(update, client, honey);
                }
                catch (MemoryCachException ex)
                {
                    await ex.SendExceptionMessage(chatId, client);
                }
                catch (TryParseException ex)
                {
                    await ex.GetAnswerForTryParseException(client,
                        _memoryCachService.GetCallbackQueryIdFromMemoryCatch());
                }
            }
        }
    }

    private void SetHoneyPrice(string price, HoneyDto honey)
    {
        if(decimal.TryParse(price, out _) == true)
        {
            honey.Price = Convert.ToDecimal(price);
            _memoryCachService.SetMemoryCach("honeyCount", honey);
            return;
        }
        if (decimal.TryParse(price, out _) == false)
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
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "<-GoBackToSetPrice")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n" +
                $"<b>Вес мёда:</b> {HoneyEnumParser.GetHoneyWeightStringValue(honey.HoneyWeight)}\n" +
                $"<b>Цена мёда:</b> {honey.Price}\n\n" +
                $"Почти готово! Укажи количество единиц товара - отправь цифру в чат.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }
}
