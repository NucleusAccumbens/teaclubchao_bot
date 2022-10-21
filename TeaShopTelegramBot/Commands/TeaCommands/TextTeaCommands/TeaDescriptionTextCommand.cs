using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;

public class TeaDescriptionTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    private readonly TeaWeightMessage _teaWeightMessage = new();

    public TeaDescriptionTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "teaDescription";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string messageText = update.Message.Text;

            try
            {
                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

                var teaDto = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    return;
                }
                if (!_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    SetTeaDescription(_textCommandService.CheckStringLessThan500(messageText), teaDto);

                    await _teaWeightMessage.GetMessage(chatId, messageId, client, teaDto);
                }                           
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private void SetTeaDescription(string description, TeaDto tea)
    {
        tea.Description = description;

        _memoryCachService.SetMemoryCach(tea);
    }
}
