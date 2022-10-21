using Application.Teas.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;
public class TeaSaveCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ICreateTeaCommand _createTeaCommand;

    private readonly AddProductMessage _productAddMessage = new();

    public TeaSaveCallbackCommand(IMemoryCachService memoryCachService, ICreateTeaCommand createTeaCommand)
    {
        _memoryCachService = memoryCachService;
        _createTeaCommand = createTeaCommand;
    }

    public override char CallbackDataCode => 'H';


    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var teaDto = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (teaDto != null && teaDto.Name != null)
                {
                    await CreateTea(teaDto);

                    await GetAnswerForSavedTea(callbackId, client, teaDto.Name);

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

    private async Task CreateTea(TeaDto teaDto)
    {
        //var tea = new Tea()
        //{
        //    Name = teaDto.Name,
        //    Description = teaDto.Description,
        //    Price = teaDto.Price,
        //    Count = teaDto.Count,
        //    PathToPhoto = teaDto.PathToPhoto,
        //    TeaForm = teaDto.Form,
        //    TeaType = te

        //};

        var tea = teaDto.Adapt<Tea>();

        await _createTeaCommand.CreateTeaAsync(tea);
    }

    private async Task GetAnswerForSavedTea(string callbackId, ITelegramBotClient client, 
        string teaName)
    {
        await MessageService.ShowAllert(callbackId, client, 
            $"✨ Чай {teaName} добавлен в список товаров! ✨");
    }

    private async Task DeleteMessage(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            await client.DeleteMessageAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId);
        }
    }
}
