using Application.Honeys.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.HoneyCommands.TextHoneyCommands;

public class EditHoneyDescriptionTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IUpdateHoneyCommand _updateHoneyCommand;

    private readonly ITextCommandService _textCommandService;

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    public EditHoneyDescriptionTextCommand(IMemoryCachService memoryCachService, IUpdateHoneyCommand updateHoneyCommand,
        ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _updateHoneyCommand = updateHoneyCommand;
        _textCommandService = textCommandService;
    }

    public override string Name => "editHoneyDescription";

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

                    honey.Description = messageText;

                    await _updateHoneyCommand.UpdateHoneyDescriptionAsync(honey.Adapt<Honey>());

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    _memoryCachService.SetMemoryCach(String.Empty, update);

                    await _productEditPageMessage.GetMessage(chatId, messageIdFromMemory,
                        client, honey, 'p');
                }
                catch (MemoryCachException ex)
                {
                    await ex.SendExceptionMessage(chatId, client);
                }
            }
        }
    }
}
