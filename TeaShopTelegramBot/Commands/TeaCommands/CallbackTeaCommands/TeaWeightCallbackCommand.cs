using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaWeightCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly TeaFormMessage _teaFormMessage = new();

    public TeaWeightCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }
    public override char CallbackDataCode => 'E';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            try
            {
                var tea = _memoryCachService.GetTeaDtoFromMemoryCach();

                if (update.CallbackQuery.Data == "E50")
                {
                    var teaDto = SetTeaWeight(TeaWeight.Fifty, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

                    return;
                }
                if (update.CallbackQuery.Data == "E100")
                {
                    var teaDto = SetTeaWeight(TeaWeight.OneHundred, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

                    return;
                }
                if (update.CallbackQuery.Data == "E150")
                {
                    var teaDto = SetTeaWeight(TeaWeight.OneHundredFifty, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

                    return;
                }
                if (update.CallbackQuery.Data == "E200")
                {
                    var teaDto = SetTeaWeight(TeaWeight.TwoHundred, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

                    return;
                }
                if (update.CallbackQuery.Data == "E250")
                {
                    var teaDto = SetTeaWeight(TeaWeight.TwoHundredFifty, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);

                    return;
                }
                if (update.CallbackQuery.Data == "E357")
                {
                    var teaDto = SetTeaWeight(TeaWeight.ThreeHundredFiftySeven, tea);

                    await _teaFormMessage.GetMessage(chatId, messageId, client, teaDto);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private TeaDto SetTeaWeight(TeaWeight teaWeight, TeaDto tea)
    {
        tea.TeaWeight = teaWeight;

        _memoryCachService.SetMemoryCach(tea);

        return _memoryCachService.GetTeaDtoFromMemoryCach();
    }
}
