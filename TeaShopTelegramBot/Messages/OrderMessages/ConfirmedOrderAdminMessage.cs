using TeaShopTelegramBot.Common.StringBuilders;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Messages.OrderMessages;

public class ConfirmedOrderAdminMessage
{
    private readonly OrderStringBuilder _builder = new();

    public async Task GetMessage(long chatId, string? username, ITelegramBotClient client, OrderDto orderDto)
    {
        string order = _builder.GetStringForOrder(orderDto, Language.Russian);

        string message = GetText(username, orderDto.Id, order);
        
        await MessageService
                .SendMessage(chatId, client, message, null);
    }

    private string GetText(string? username, long? orderId, string order)
    {
        return $"{order}\n\n" +
            $"<b>От пользователя:</b> @{username}"; 
    }

    //private InlineKeyboardMarkup GetInlineKeyboardMarkup(long? orderId)
    //{
    //    return new(new[]
    //    {
    //        new[]
    //        {
    //            InlineKeyboardButton.WithCallbackData(text: "🤝 Сделка состоялась 🤝", callbackData: $"cArchive{orderId}"),
    //        },
    //    });
    //}
}
