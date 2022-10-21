namespace TeaShopTelegramBot.Common.Servicies;

public class TextCommandService : ITextCommandService
{
    public bool CheckMessageIsCommand(string message)
    {
        string[] messageCollection = new string[]
        { "/start", "/add_product", "/edit_products", "/discounts", "/discount",
            "/small_wholesale", "/language", "/menu", "/cart", "/help"};

        for (int i = 0; i < messageCollection.Length; i++)
        {
            if (messageCollection[i].Contains(message))
            {
                return true;
            }
        }
        return false;
    }

    public string CheckStringLessThan500(string message)
    {
        if (message.Length > 500)
        {
            return message.Substring(0, 500);
        }

        return message;
    }
}
