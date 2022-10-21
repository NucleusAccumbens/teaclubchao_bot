using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;
public class TeaNameTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    private readonly TeaDescriptionMessage _teaDescriptionMessage = new();

    public TeaNameTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "teaName";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            string messageText = update.Message.Text;

            try
            {
                var teaDto = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    return;
                }
                if (!_textCommandService.CheckMessageIsCommand(messageText))
                {

                    SetTeaNameAndCommandState(_textCommandService.CheckStringLessThan500(messageText), teaDto);

                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    await _teaDescriptionMessage.GetMessage(chatId, messageId, client, teaDto);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }  

    private void SetTeaNameAndCommandState(string teaName, TeaDto teaDto)
    {
        teaDto.Name = teaName;

        _memoryCachService.SetMemoryCach("teaDescription", teaDto);
    }
}
