using Application.Herbs.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class HerbSaveCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ICreateHerbCommand _createHerbCommand;

    private readonly AddProductMessage _productAddMessage = new();

    public HerbSaveCallbackCommand(IMemoryCachService memoryCachService, ICreateHerbCommand createHerbCommand)
    {
        _memoryCachService = memoryCachService;
        _createHerbCommand = createHerbCommand;
    }

    public override char CallbackDataCode => 'K';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var herbDto = _memoryCachService.GetHerbDtoFromMemoryCach();

                if (herbDto != null && herbDto.Name != null)
                {
                    await CreateHerb(herbDto);

                    await GetAnswerForSavedHerb(callbackId, client, herbDto.Name);

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

    private async Task CreateHerb(HerbDto herbDto)
    {
        var herb = herbDto.Adapt<Herb>();

        await _createHerbCommand.CreateHerbAsync(herb);
    }

    private async Task GetAnswerForSavedHerb(string callbackId, ITelegramBotClient client,
        string name)
    {
        await MessageService.ShowAllert(callbackId, client,
           $"✨ Сбор {name} добавлен в список товаров! ✨");
    }
}
