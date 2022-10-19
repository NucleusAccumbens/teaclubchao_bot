using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaPhotoMessage
{
    private readonly string _messageText = $"Почти готово! Осталось только загрузить фотографию. " +
        $"Отправь её сообщением в этот чат.";


    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "_GoBackToSetCount")
        },
    });


    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n\n";

        await MessageService.EditMessage(chatId, messageId, client,
            $"{teaInfo}{_messageText}", _inlineKeyboardMarkup);
    }
}
