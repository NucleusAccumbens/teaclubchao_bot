using Application.Honeys.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;

namespace TeaShopTelegramBot.Commands.HoneyCommands.CallbackHoneyCommands;

public class HoneyAddCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public override char CallbackDataCode => 'C';

    public HoneyAddCallbackCommand(IMemoryCachService memoryCacheService)
    {
        _memoryCachService = memoryCacheService;
    }

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {               
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ".AddProduct")
                },
            });

            await client.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: "Чтобы установить название мёда, отправь его сообщением в этот чат.",
                replyMarkup: inlineKeyboardMarkup);

            _memoryCachService.SetMemoryCach("honeyName", update);
        }
    }
}

