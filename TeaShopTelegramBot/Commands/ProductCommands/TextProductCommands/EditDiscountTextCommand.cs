using Application.Herbs.Interfaces;
using Application.Honeys.Interfaces;
using Application.Products.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;
using Mapster;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.ProductMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.ProductCommands.TextProductCommands;

public class EditDiscountTextCommand : BaseTextCommand
{
    private readonly EditDiscountPageMessage _editDiscountPageMessage = new();

    private readonly ITextCommandService _textCommandService;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IUpdateHerbCommand _updateHerbCommand;

    private readonly IUpdateTeaCommand _updateTeaCommand;

    private readonly IUpdateHoneyCommand _updateHoneyCommand;

    public EditDiscountTextCommand(ITextCommandService textCommandService, IMemoryCachService memoryCachService,
        IUpdateHoneyCommand updateHoneyCommand, IUpdateHerbCommand updateHerbCommand, IUpdateTeaCommand updateTeaCommand)
    {
        _textCommandService = textCommandService;
        _memoryCachService = memoryCachService;
        _updateHerbCommand = updateHerbCommand;
        _updateTeaCommand = updateTeaCommand;
        _updateHoneyCommand = updateHoneyCommand;
    }

    public override string Name => "editDiscount";

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

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    var productDto = _memoryCachService.GetProductDtoFromMemoryCach();

                    productDto.Discount = ConvertStringToInt32(messageText);

                    if (productDto is TeaDto) await _updateTeaCommand
                            .UpdateTeaDiscountAsync(productDto.Adapt<Tea>());

                    if (productDto is HerbDto) await _updateHerbCommand
                            .UpdateHerbDiscountAsync(productDto.Adapt<Herb>());

                    if (productDto is HoneyDto) await _updateHoneyCommand
                            .UpdateHoneyDiscountAsync(productDto.Adapt<Honey>());

                    _memoryCachService.SetMemoryCach(String.Empty, update);

                    await _editDiscountPageMessage.GetMessage(chatId, messageIdFromMemory, client, productDto, 's');

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
