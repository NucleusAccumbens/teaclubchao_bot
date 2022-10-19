using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.TextHerbCommands;
public class HerbCountTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public HerbCountTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "herbCount";

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
                    await DeleteMessage(update, client);

                    SetHerbCountAndCommandState(messageText, herb);

                    await EditMenuMessage(update, client, herb);
                }
            }
            catch (TryParseException ex)
            {
                await ex.GetAnswerForTryParseException(client,
                    _memoryCachService.GetCallbackQueryIdFromMemoryCatch());
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
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ",GoBackToSetCount")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.Message.Chat.Id,
                messageId: messageId,
                text: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n" +
                $"<b>Вес сбора:</b> {HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n" +
                $"<b>Цена сбора:</b> {herbDto.Price}\n" +
                $"<b>Количество сбора:</b> {herbDto.Count}\n\n" +
                $"Почти готово! Осталось только загрузить фотографию. " +
                $"Отправь её сообщением в этот чат.",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);
        }
    }

    private void SetHerbCountAndCommandState(string count, HerbDto herbDto)
    {
        if (int.TryParse(count, out _))
        {
            herbDto.Count = Convert.ToInt32(count);
            _memoryCachService.SetMemoryCach("herbPhoto", herbDto);
        }

        else throw new TryParseException();
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
}
