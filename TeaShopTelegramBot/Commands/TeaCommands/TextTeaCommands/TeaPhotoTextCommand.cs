using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;


namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;
public class TeaPhotoTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly SaveTeaMessage _saveTeaMessage = new();

    public TeaPhotoTextCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override string Name => "teaPhoto";


    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Photo != null)
        {
            long chatId = update.Message.Chat.Id;

            try
            {
                var tea = _memoryCachService.GetTeaDtoFromMemoryCach();

                SetTeaPhoto(update.Message.Photo[2].FileId, tea);

                await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                await EditMenuMessage(update, client, tea);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client,  
        TeaDto teaDto)
    {
        if (update.Message != null && teaDto.PathToPhoto != null)
        {
            var messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

            await MessageService.DeleteMessage(update.Message.Chat.Id, messageId, client);

            await _saveTeaMessage.GetMessage(update.Message.Chat.Id, client, teaDto);

            _memoryCachService.SetMemoryCach(String.Empty, update);

            _memoryCachService.SetMemoryCach(teaDto);
        }
    }

    private void SetTeaPhoto(string pathToPhoto, TeaDto tea)
    {
        tea.PathToPhoto = pathToPhoto;

        _memoryCachService.SetMemoryCach(tea);
    }
}
