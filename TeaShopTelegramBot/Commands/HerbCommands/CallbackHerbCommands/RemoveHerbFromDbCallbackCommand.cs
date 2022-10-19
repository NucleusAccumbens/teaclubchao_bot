using Application.Herbs.Interfaces;
using Application.Products.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class RemoveHerbFromDbCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IDeleteProductCommand _deleteProductCommand;

    private readonly IGetHerbQuery _getHerbQuery;

    public RemoveHerbFromDbCallbackCommand(IDeleteProductCommand deleteProductCommand, IGetHerbQuery getHerbQuery)
    {
        _deleteProductCommand = deleteProductCommand;
        _getHerbQuery = getHerbQuery;
    }

    public override char CallbackDataCode => 'o';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data.Contains("oRemove"))
            {
                await _deleteProductCommand
                    .DeleteProductAsync(GetHerbIdForRemove(update.CallbackQuery.Data));

                await MessageService.ShowAllert(callbackId, client,
                    "Сбор удалён из базы данных!");

                await MessageService.DeleteMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data.Contains("oBack"))
            {
                var herb = await _getHerbQuery
                    .GetHerbAsync(GetHerbIdForGoBack(update.CallbackQuery.Data));

                await _productEditPageMessage.GetMessage(chatId, messageId, client,
                    herb.Adapt<HerbDto>(), 'm');
            }
        }
    }

    private long GetHerbIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetHerbIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }
}
