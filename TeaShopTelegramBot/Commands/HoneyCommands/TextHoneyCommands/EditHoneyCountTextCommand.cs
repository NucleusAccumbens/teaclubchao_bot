using Application.Honeys.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;

public class EditHoneyCountTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IUpdateHoneyCommand _updateHoneyCommand;

    private readonly ITextCommandService _textCommandService;

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    public EditHoneyCountTextCommand(IMemoryCachService memoryCachService, IUpdateHoneyCommand updateHoneyCommand,
        ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _updateHoneyCommand = updateHoneyCommand;
        _textCommandService = textCommandService;
    }

    public override string Name => "editHoneyCount";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = update.Message.MessageId;

            string messageText = update.Message.Text;

            if (_textCommandService.CheckMessageIsCommand(messageText))
            {
                await MessageService.DeleteMessage(chatId, messageId, client);

                return;
            }
            if (!_textCommandService.CheckMessageIsCommand(messageText))
            {
                try
                {
                    var messageIdFromMemory = _memoryCachService.GetMessageIdFromMemoryCatch();

                    var honey = _memoryCachService.GetHoneyDtoFromMemoryCach();

                    honey.Count = ConvertStringToInt32(messageText);

                    await _updateHoneyCommand.UpdateHoneyCountAsync(honey.Adapt<Honey>());

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    _memoryCachService.SetMemoryCach(String.Empty, update);

                    await _productEditPageMessage.GetMessage(chatId, messageIdFromMemory,
                        client, honey, 'p');
                }
                catch (MemoryCachException ex)
                {
                    await ex.SendExceptionMessage(chatId, client);
                }
                catch (TryParseException)
                {
                    await MessageService.DeleteMessage(chatId, messageId, client);
                }
            }
        }
    }

    private static int ConvertStringToInt32(string data)
    {
        if (Int32.TryParse(data, out _))
            return Convert.ToInt32(data);

        else throw new TryParseException();
    }
}
