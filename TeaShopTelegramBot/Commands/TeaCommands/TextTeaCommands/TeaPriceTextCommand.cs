using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.TextTeaCommands;
public class TeaPriceTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly ITextCommandService _textCommandService;

    private readonly TeaCountMessage _teaCountMessage = new();

    public TeaPriceTextCommand(IMemoryCachService memoryCachService, ITextCommandService textCommandService)
    {
        _memoryCachService = memoryCachService;

        _textCommandService = textCommandService;
    }

    public override string Name => "teaPrice";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string messageText = update.Message.Text;

            try
            {
                var teaDto = _memoryCachService.GetTeaDtoFromMemoryCach();

                int messageId = _memoryCachService.GetMessageIdFromMemoryCatch();

                if (_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    return;
                }
                if (!_textCommandService.CheckMessageIsCommand(messageText))
                {
                    await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

                    SetTeaPriceAndCommandState(messageText, teaDto);

                    await _teaCountMessage.GetMessage(chatId, messageId, client, teaDto);
                }             
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
            catch (TryParseException ex)
            {
                await ex.GetAnswerForTryParseException(client,
                    _memoryCachService.GetUpdateFromMemoryCach().CallbackQuery.Id);
            }
        }
    }

    private void SetTeaPriceAndCommandState(string price, TeaDto teaDto)
    {
        if (decimal.TryParse(price, out _))
        {
            teaDto.Price = Convert.ToDecimal(price);

            _memoryCachService.SetMemoryCach("teaCount", teaDto);
        }

        else throw new TryParseException();
    }
}