using Application.Products.Interfaces;
using Application.Teas.Interfaces;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class RemoveTeaFromDbCallbackCommand : BaseCallbackCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IDeleteProductCommand _deleteProductCommand;

    private readonly IGetTeaQuery _getTeaQuery;

    public RemoveTeaFromDbCallbackCommand(IDeleteProductCommand deleteProductCommand, IGetTeaQuery getTeaQuery)
    {
        _deleteProductCommand = deleteProductCommand;
        _getTeaQuery = getTeaQuery;
    }

    public override char CallbackDataCode => 'k';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data.Contains("kRemove"))
            {
                await _deleteProductCommand
                    .DeleteProductAsync(GetTeaIdForRemove(update.CallbackQuery.Data));
                
                await MessageService.ShowAllert(callbackId, client,
                    "Чай удалён из базы данных!");

                await MessageService.DeleteMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data.Contains("kBack"))
            {
                var tea = await _getTeaQuery
                    .GetTeaAsync(GetTeaIdForGoBack(update.CallbackQuery.Data));

                await _productEditPageMessage.GetMessage(chatId, messageId, client,
                    tea.Adapt<TeaDto>(), 'i');
            }
        }
    }

    private long GetTeaIdForRemove(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private long GetTeaIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }
}
