using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaNameMessage
{   
    private readonly string _messageText = "Отправь название чая сообщением в этот чат.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "A")
        },
    });

    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n";

        await MessageService.EditMessage(chatId, messageId, client,
            $"{teaInfo}{_messageText}", _inlineKeyboardMarkup);
    }
}
