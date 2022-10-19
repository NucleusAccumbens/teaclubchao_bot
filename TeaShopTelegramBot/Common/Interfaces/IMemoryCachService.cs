using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(string commandState, Update update);
    void SetMemoryCach(string commandState, TeaDto teaDto);
    void SetMemoryCach(TeaDto teaDto);
    void SetMemoryCach(string commandState, HerbDto herbDto);
    void SetMemoryCach(HerbDto herbDto);
    void SetMemoryCach(ProductDto productDto);
    void SetMemoryCach(string commandState, Update update, HoneyDto honeyDto);
    void SetMemoryCach(long chatId, string commandState);
    void SetMemoryCach(long chatId, int messageId);
    void SetMemoryCach(string commandState, HoneyDto honeyDto);
    void SetMemoryCach(HoneyDto honeyDto);
    void SetMemoryCach(long chatId, OrderDto orderDto);
    string? GetCommandStateFromMemoryCach();
    string? GetCommandStateFromMemoryCach(long chatId);
    Update GetUpdateFromMemoryCach();
    int GetMessageIdFromMemoryCatch();
    int GetMessageIdFromMemoryCatch(long chatId);
    string GetCallbackQueryIdFromMemoryCatch();
    ProductDto GetProductDtoFromMemoryCach();
    TeaDto GetTeaDtoFromMemoryCach();
    HerbDto GetHerbDtoFromMemoryCach();
    HoneyDto GetHoneyDtoFromMemoryCach();
    OrderDto? GetOrderDtoFromMemoryCach(long chatId);
}

