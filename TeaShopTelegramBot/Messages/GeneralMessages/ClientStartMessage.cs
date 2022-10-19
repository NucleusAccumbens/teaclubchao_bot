namespace TeaShopTelegramBot.Messages.GeneralMessages;

public class ClientStartMessage
{
    private readonly string _russianMessageText =
        "✨ Привет, Дорогой чайный друг! " +
        "Добро пожаловать в наш чайный бот! ✨\n\n" +
        "Выбирай чаи на любой вкус! 🙏🏻⛩🙏🏻\n\n" +
        "Чтобы ознакомиться с ассортиментом и совершить покупку, переходи в " +
        "/menu";

    private readonly string _englishMessageText =
        "✨ Hello Dear Tea Friend! " +
        "Welcome to our tea bot! ✨\n\n" +
        "Choose teas for every taste! 🙏🏻⛩🙏🏻\n\n" +
        "To see the range and make a purchase, go to " +
        "/menu";

    private readonly string _hebrewMessageText =
        "✨ שלום חבר תה יקר! " +
        "ברוך הבא לבוט התה שלנו! ✨\n\n" +
        "בחרו תה לכל טעם! 🙏🏻⛩🙏🏻\n\n" +
        "כדי לראות את הטווח ולבצע רכישה, עבור אל " +
        "/menu";


    public async Task GetMessage(long chatId, ITelegramBotClient client, Language? language)
    {
        if (language == Language.Russian) await MessageService
                .SendMessage(chatId, client, _russianMessageText, null);

        if (language == Language.English) await MessageService
                .SendMessage(chatId, client, _englishMessageText, null);

        if (language == Language.Hebrew) await MessageService
                .SendMessage(chatId, client, _hebrewMessageText, null);
    }
}
