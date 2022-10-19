using Application.Honeys.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class HoneySaveCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ICreateHoneyCommand _createHoneyCommand;

    private readonly AddProductMessage _productAddMessage = new();

    public HoneySaveCallbackCommand(IMemoryCachService memoryCachService, ICreateHoneyCommand createHoneyCommand)
    {
        _memoryCachService = memoryCachService;
        _createHoneyCommand = createHoneyCommand;
    }

    public override char CallbackDataCode => 'L';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var honeyDto = _memoryCachService.GetHoneyDtoFromMemoryCach();

                if (honeyDto != null && honeyDto.Name != null)
                {
                    await CreateHoney(honeyDto);

                    await GetAnswerForSavedTea(callbackId, client, honeyDto.Name);

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    await _productAddMessage.GetMessage(chatId, client);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }         
        }
    }

    private async Task GetAnswerForSavedTea(string callbackId, ITelegramBotClient client,
        string name)
    {
        await MessageService.ShowAllert(callbackId, client,
           $"✨ Мёд {name} добавлен в список товаров! ✨");
    }

    private async Task CreateHoney(HoneyDto honeyDto)
    {
        var honey = honeyDto.Adapt<Honey>();

        await _createHoneyCommand.CreateHoneyAsync(honey);
    }
}
