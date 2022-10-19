using Domain.Entities;

namespace TeaShopTelegramBot.Models;

public class ContactsDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public string? Address { get; set; }
}
