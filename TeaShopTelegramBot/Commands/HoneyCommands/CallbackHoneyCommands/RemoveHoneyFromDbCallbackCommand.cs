using Application.Herbs.Interfaces;
using Application.Honeys.Interfaces;
using Application.Products.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class RemoveHoneyFromDbCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IDeleteProductCommand _deleteProductCommand;

    private readonly IGetHoneyQuery _getHoneyQuery;

    public RemoveHoneyFromDbCallbackCommand(IDeleteProductCommand deleteProductCommand,
        IGetHoneyQuery getHoneyQuery)
    {
        _deleteProductCommand = deleteProductCommand;
        _getHoneyQuery = getHoneyQuery;
    }

    public override char CallbackDataCode => 'r';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data.Contains("rRemove"))
            {
                await _deleteProductCommand
                    .DeleteProductAsync(GetHoneyIdForRemove(update.CallbackQuery.Data));

                await MessageService.ShowAllert(callbackId, client,
                    "Мёд удалён из базы данных!");

                await MessageService.DeleteMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data.Contains("rBack"))
            {
                var honey = await _getHoneyQuery
                    .GetHoneyAsync(GetHoneyIdForGoBack(update.CallbackQuery.Data));

                await _productEditPageMessage.GetMessage(chatId, messageId, client,
                    honey.Adapt<HoneyDto>(), 'p');
            }
        }
    }

    private long GetHoneyIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetHoneyIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }
}
