using Application.Common.EnumParsers;
using Application.TlgUsers.Interfaces;
using Domain.Enums;
using TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;
public class TeaFormCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

private readonly TeaPriceMessage _teaPriceMessage = new();

    public TeaFormCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }
    public override char CallbackDataCode => 'G';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            try
            {
                var tea = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (update.CallbackQuery.Data == "GPressed")
                {
                    SetTeaForm(TeaForms.Pressed, tea);

                    _memoryCachService.SetMemoryCach("teaPrice", update);

                    await _teaPriceMessage.GetMessage(chatId, messageId, client, tea);

                    return;
                }
                if (update.CallbackQuery.Data == "GLoose")
                {
                    SetTeaForm(TeaForms.Loose, tea);

                    await _teaPriceMessage.GetMessage(chatId, messageId, client, tea);

                    _memoryCachService.SetMemoryCach("teaPrice", update);

                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private void SetTeaForm(TeaForms teaForm, TeaDto teaDto)
    {
        teaDto.TeaForm = teaForm;

        _memoryCachService.SetMemoryCach(teaDto);
    }
   
}
