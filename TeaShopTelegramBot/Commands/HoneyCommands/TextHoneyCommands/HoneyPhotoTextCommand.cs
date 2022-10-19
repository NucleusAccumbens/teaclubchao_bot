using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;

public class HoneyPhotoTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;
    public override string Name => "honeyPhoto";

    public HoneyPhotoTextCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Photo != null)
        {
            long chatId = update.Message.Chat.Id;
            
            try
            {
                var honey = _memoryCachService.GetHoneyDtoFromMemoryCach();

                SetHoneyPhoto(update.Message.Photo[2].FileId, honey);

                await DeleteMessage(update, client);

                await EditMenuMessage(update, client, honey);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private void SetHoneyPhoto(string pathToPhoto, HoneyDto honey)
    {
        honey.PathToPhoto = pathToPhoto;
        _memoryCachService.SetMemoryCach(honey);
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
        if (update.Message != null && honey.PathToPhoto != null)
        {
            var messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            await client.DeleteMessageAsync(
                chatId: update.Message.Chat.Id,
                messageId: messageId);

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "✖️ Отменить", callbackData: "?"),
                    InlineKeyboardButton.WithCallbackData(text: "✔️ Сохранить", callbackData: "LSaveHoney")
                },
            });

            await client.SendPhotoAsync(
                chatId: update.Message.Chat.Id,
                photo: honey.PathToPhoto,
                caption: $"<b>Название мёда:</b> {honey.Name}\n" +
                $"<b>Описание мёда:</b> {honey.Description}\n" +
                $"<b>Вес мёда:</b> {HoneyEnumParser.GetHoneyWeightStringValue(honey.HoneyWeight)}\n" +
                $"<b>Цена мёда:</b> {honey.Price}\n" +
                $"<b>Количество мёда:</b> {honey.Count}\n\n" +
                $"Готово! Чтобы добавить мёд в ассортимент, нажми <b>\"Сохранить\"</b>",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach(String.Empty, update, honey);
        }
    }
}
