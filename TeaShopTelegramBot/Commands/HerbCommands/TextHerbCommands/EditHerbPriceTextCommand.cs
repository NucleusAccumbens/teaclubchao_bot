using Application.Herbs.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.HerbCommands.TextHerbCommands;

public class EditHerbPriceTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IUpdateHerbCommand _updateHerbCommand;

    private readonly ITextCommandService _textCommandService;

    private readonly ProductEditPageMessage _productEditPageMessage = new();

    public EditHerbPriceTextCommand(IMemoryCachService memoryCachService, IUpdateHerbCommand updateHerbCommand,
        ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;
        _updateHerbCommand = updateHerbCommand;
        _textCommandService = textCommandService;
    }

    public override string Name => "editHerbPrice";

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

                    var herb = _memoryCachService.GetHerbDtoFromMemoryCach();

                    herb.Price = ConvertStringToDecimal(messageText);

                    await _updateHerbCommand.UpdateHerbPriceAsync(herb.Adapt<Herb>());

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    _memoryCachService.SetMemoryCach(String.Empty, update);

                    await _productEditPageMessage.GetMessage(chatId, messageIdFromMemory,
                        client, herb, 'm');
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

    private static decimal ConvertStringToDecimal(string data)
    {
        if (Decimal.TryParse(data, out _))
            return Convert.ToDecimal(data);

        else throw new TryParseException();
    } 
}
