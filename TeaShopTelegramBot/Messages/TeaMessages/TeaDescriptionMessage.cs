using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaDescriptionMessage
{
    private readonly string _messageText = "Теперь отправь сообщение с описанием чая.\n\n" +
        "<i>(Длина текста не должна привышать 500 символов)</i>";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "_GoBackToSetName")
        },
    });


    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n";

        await MessageService.EditMessage(chatId, messageId, client,
            $"{teaInfo}{_messageText}", _inlineKeyboardMarkup);
    }
}
