using Application.Herbs.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;

namespace TeaShopTelegramBot.Commands.HerbCommands.TextHerbCommands;

public class HerbDiscountTextCommand : BaseTextCommand
{
    private readonly ProductEditPageMessage _productEditPageMessage = new();

    private readonly IUpdateHerbCommand _updateHerbCommand;

    private readonly ITextCommandService _textCommandService;

    private readonly IMemoryCachService _memoryCachService;

    public HerbDiscountTextCommand(IUpdateHerbCommand updateHerbCommand, ITextCommandService textCommandService,
        IMemoryCachService memoryCachService)
    {
        _updateHerbCommand = updateHerbCommand;
        _textCommandService = textCommandService;
        _memoryCachService = memoryCachService;
    }

    public override string Name => "herbDiscount";

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

                    var herb = _memoryCachService.GetHerbDtoFromMemoryCach();

                    herb.Discount = ConvertStringToInt32(messageText);

                    await _updateHerbCommand.UpdateHerbDiscountAsync(herb.Adapt<Herb>());

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

    private static int ConvertStringToInt32(string data)
    {
        if (Int32.TryParse(data, out _))
            return Convert.ToInt32(data);

        else throw new TryParseException();
    }
}
