using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;


namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;
public class TeaCountTextCommand : BaseTextCommand
{
    private readonly TeaPhotoMessage _teaPhotoMessage = new();
    
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    public TeaCountTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _textCommandService = textCommandService;
    }

    public override string Name => "teaCount";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            try
            {
                var teaDto = _memoryCachService.GetTeaDtoFromMemoryCach();

                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

                string messageText = update.Message.Text;

                if (_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    return;
                }
                if (!_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    SetTeaCountAndCommandState(messageText, teaDto);

                    await _teaPhotoMessage.GetMessage(chatId, messageId, client, teaDto);
                }             
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
            catch (TryParseException ex)
            {
                await ex.GetAnswerForTryParseException(client,
                    _memoryCachService.GetCallbackQueryIdFromMemoryCatch());
            }
        }
            
    }

    private void SetTeaCountAndCommandState(string count, TeaDto teaDto)
    {
        if (int.TryParse(count, out _))
        {
            teaDto.Count = Convert.ToInt32(count);

            _memoryCachService.SetMemoryCach("teaPhoto", teaDto);
        }

        else throw new TryParseException();
    }
}
