using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class SaveTeaMessage
{
    private readonly string _messageText = $"Готово! Чтобы добавить чай в ассортимент, нажми <b>\"Сохранить\"</b>";


    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✖️ Отменить", callbackData: "?"),
            InlineKeyboardButton.WithCallbackData(text: "✔️ Сохранить", callbackData: "HSaveTea")
        },
    });


    public async Task GetMessage(long chatId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n\n";

        await MessageService.SendMessage(chatId, client,
            $"{teaInfo}{_messageText}", teaDto.PathToPhoto, _inlineKeyboardMarkup);
    }
}
