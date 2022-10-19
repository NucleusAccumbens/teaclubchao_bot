using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaFormMessage
{
    private readonly string _messageText = "Теперь выбери форму хранения чая.";


    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Пресованный", callbackData: "GPressed"),
            InlineKeyboardButton.WithCallbackData(text: "Рассыпной", callbackData: "GLoose"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "_GoBackToSetWeight")
        },
    });


    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n";

        await MessageService.EditMessage(chatId, messageId, client,
            $"{teaInfo}{_messageText}", _inlineKeyboardMarkup);
    }
}
