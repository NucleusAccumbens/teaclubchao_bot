using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;
public class TeaRegressionCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly TeaNameMessage _teaNameMessage = new();

    private readonly TeaDescriptionMessage _teaDescriptionMessage = new();

    private readonly TeaWeightMessage _teaWeightMessage = new();

    private readonly TeaFormMessage _teaFormMessage = new();

    private readonly TeaPriceMessage _teaPriceMessage = new();

    private readonly TeaCountMessage _teaCountMessage = new();


    public TeaRegressionCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => '_';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null &&  update.CallbackQuery.Data != null)
        {
            try
            {
                var tea = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (update.CallbackQuery.Data == "_GoBackToSetName")
                {
                    await GoBackToSetName(update, client, tea);

                    return;
                }
                if (update.CallbackQuery.Data == "_GoBackToSetDescription")
                {
                    await GoBackToSetDescription(update, client, tea);

                    return;
                }
                if (update.CallbackQuery.Data == "_GoBackToSetWeight")
                {
                    await GoBackToSetWeight(update, client, tea);
                    return;
                }
                if (update.CallbackQuery.Data == "_GoBackToSetForm")
                {
                    await GoBackToSetForm(update, client, tea);

                    return;
                }
                if (update.CallbackQuery.Data == "_GoBackToSetPrice")
                {
                    await GoBackToSetPrice(update, client, tea);

                    return;
                }
                if (update.CallbackQuery.Data == "_GoBackToSetCount")
                {
                    await GoBackToSetCount(update, client, tea);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(update.CallbackQuery.Message.Chat.Id, client);
            } 
        }
    }

    private async Task GoBackToSetName(Update update, ITelegramBotClient client,
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            teaDto.Name = null;

            await _teaNameMessage.GetMessage(chatId, messageId, client, teaDto);

            _memoryCachService.SetMemoryCach("teaName", update);
        }
    }

    private async Task GoBackToSetDescription(Update update, ITelegramBotClient client,
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
            {
                long chatId = update.CallbackQuery.Message.Chat.Id;

                int messageId = update.CallbackQuery.Message.MessageId;

                teaDto.Description = null;

                await _teaDescriptionMessage.GetMessage(chatId, messageId, client, teaDto);

                _memoryCachService.SetMemoryCach("teaDescription", update);
            }
        }
    }

    private async Task GoBackToSetWeight(Update update, ITelegramBotClient client,
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            teaDto.TeaWeight = null;

            await _teaWeightMessage.GetMessage(chatId, messageId, client, teaDto);

            _memoryCachService.SetMemoryCach(String.Empty, update);
        }
    }

    private async Task GoBackToSetForm(Update update, ITelegramBotClient client, 
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            teaDto.TeaForm = null;

            await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

            _memoryCachService.SetMemoryCach(String.Empty, update);
        }
    }

    private async Task GoBackToSetPrice(Update update, ITelegramBotClient client, 
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            teaDto.Price = null;

            await _teaPriceMessage.GetMessage(chatId, messageId, client, teaDto);

            _memoryCachService.SetMemoryCach("teaPrice", update);
        }
    }


    private async Task GoBackToSetCount(Update update, ITelegramBotClient client, 
        TeaDto teaDto)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            teaDto.Count = null;

            await _teaCountMessage.GetMessage(chatId, messageId, client, teaDto);

            _memoryCachService.SetMemoryCach("teaCount", update);
        }
    }
}