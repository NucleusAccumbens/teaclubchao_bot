namespace TeaShopTelegramBot.Messages.AdminMessages;

public class StartAdminMessage
{
    private readonly string _text = 
        "Привет, админ! 🖖🏻\n\n" +
        "Управление ботом доступно только администраторам.\n\n" +
        "🔥 Используй соответствующие команды, чтобы добавить товар, " +
        "редактировать информацию о товарах, " +
        "а также чтобы управлять скидками 🔥\n\n" +
        "/add_product - добавить новый товар\n" +
        "/edit_products - редактировать товары\n" +
        "/discounts - управление скидками";


    public async Task GetMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService.SendMessage(chatId, client, _text, null);
    }
}
