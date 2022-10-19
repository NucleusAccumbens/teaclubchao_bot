using Domain.Enums;

namespace TeaShopTelegramBot.Models;
public class TeaDto : ProductDto
{
    public TeaWeight? TeaWeight { get; set; }
    public TeaForms? TeaForm { get; set; }
    public TeaTypes? TeaType { get; set; }
}
