using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.TextHerbCommands;
public class HerbPhotoTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;
    public override string Name => "herbPhoto";

    public HerbPhotoTextCommand(IMemoryCachService memoryCachService)
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
                var herb = _memoryCachService.GetHerbDtoFromMemoryCach();

                SetHerbPhoto(update.Message.Photo[2].FileId, herb);

                await DeleteMessage(update, client);

                await EditMenuMessage(update, client, herb);
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
        if (update.Message != null && herbDto.PathToPhoto != null)
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
                    InlineKeyboardButton.WithCallbackData(text: "✔️ Сохранить", callbackData: "KSaveHerb")
                },
            });

            await client.SendPhotoAsync(
                chatId: update.Message.Chat.Id,
                photo: herbDto.PathToPhoto,
                caption: $"<b>Регион сбора:</b> {HerbEnumParser.GetHerbRegionStringValueInRussian(herbDto.Region)}\n" +
                $"<b>Название сбора:</b> {herbDto.Name}\n" +
                $"<b>Описание сбора:</b> {herbDto.Description}\n" +
                $"<b>Вес сбора:</b> {HerbEnumParser.GetHerbWeightStringValue(herbDto.Weight)}\n" +
                $"<b>Цена сбора:</b> {herbDto.Price}\n" +
                $"<b>Количество сбора:</b> {herbDto.Count}\n\n" +
                $"Готово! Чтобы добавить сбор в ассортимент, нажми <b>\"Сохранить\"</b>",
                replyMarkup: inlineKeyboardMarkup,
                parseMode: ParseMode.Html);

            _memoryCachService.SetMemoryCach(String.Empty, update);
            _memoryCachService.SetMemoryCach(herbDto);
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

    private void SetHerbPhoto(string pathToPhoto, HerbDto herbDto)
    {
        herbDto.PathToPhoto = pathToPhoto;
        _memoryCachService.SetMemoryCach(herbDto);
    }
}
