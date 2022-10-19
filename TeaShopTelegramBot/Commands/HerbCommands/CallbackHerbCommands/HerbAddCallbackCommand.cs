using TeaShopTelegramBot.Common.Abstractions;

namespace TeaShopTelegramBot.Commands.HerbCommands.CallbackHerbCommands;

public class HerbAddCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public HerbAddCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'B';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            _memoryCachService.SetMemoryCach(String.Empty, update);
            
            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🏔️ Алтай 🏔️", callbackData: "IAltai"),
                    InlineKeyboardButton.WithCallbackData(text: "⛰️ Кавказ ⛰️", callbackData: "ICaucasus"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🌲 Карелия 🌲", callbackData: "IKarelia"),
                    InlineKeyboardButton.WithCallbackData(text: "🗻 Сибирь 🗻", callbackData: "ISiberia"),
                },
                new[]
                {
                        InlineKeyboardButton.WithCallbackData(text: "🔙 Назад", callbackData: ".AddProduct")
                },
            });

            await client.EditMessageTextAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    messageId: update.CallbackQuery.Message.MessageId,
                    text: "Травы какого региона хочешь добавить в ассортимент?",
                    replyMarkup: inlineKeyboardMarkup);
        }
    }
}

