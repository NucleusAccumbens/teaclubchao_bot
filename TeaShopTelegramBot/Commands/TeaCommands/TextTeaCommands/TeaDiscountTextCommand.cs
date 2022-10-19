using Application.Teas.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;

public class TeaDiscountTextCommand : BaseTextCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IUpdateTeaCommand _updateTeaCommand;

    private readonly ITextCommandService _textCommandService;

    private readonly IMemoryCachService _memoryCachService;

    public TeaDiscountTextCommand(IUpdateTeaCommand updateTeaCommand, ITextCommandService textCommandService,
        IMemoryCachService memoryCachService)
    {
        _updateTeaCommand = updateTeaCommand;
        _textCommandService = textCommandService;
        _memoryCachService = memoryCachService;
    }

    public override string Name => "teaDiscount";

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
                    int messageIdFromMemory = _memoryCachService.GetMessageIdFromMemoryCatch();

                    var tea = _memoryCachService.GetTeaDtoFromMemoryCach();

                    tea.Discount = ConvertStringToInt32(messageText);

                    await _updateTeaCommand.UpdateTeaDiscountAsync(tea.Adapt<Tea>());

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    _memoryCachService.SetMemoryCach(String.Empty, update);

                    await _productEditPageMessage.GetMessage(chatId, messageIdFromMemory,
                        client, tea, 'i');
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
