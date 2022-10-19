using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.TeaMessages;

public class TeaWeightMessage
{
    private readonly string _messageText = "Выбери вес чая.";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 50 г", callbackData: "E50"),
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 100 г", callbackData: "E100"),
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 150 г", callbackData: "E150"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 200 г", callbackData: "E200"),
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 250 г", callbackData: "E250"),
            InlineKeyboardButton.WithCallbackData(text: "⚖️ 357 г", callbackData: "E357"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: "_GoBackToSetDescription")
        },
    });


    public async Task GetMessage(long chatId, int messageId, ITelegramBotClient client, TeaDto teaDto)
    {
        string teaInfo = $"{TeaStringBuilder.GetStringForTea(teaDto, Language.Russian)}\n";

        await MessageService.EditMessage(chatId, messageId, client,
            $"{teaInfo}{_messageText}", _inlineKeyboardMarkup);
    }
}
