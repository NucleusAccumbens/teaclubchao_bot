using Microsoft.Extensions.Caching.Memory;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Models;
using Telegram.Bot.Types;

namespace TeaShopTelegramBot.Common.Servicies;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public string? GetCommandStateFromMemoryCach()
    {
        return (string)_memoryCach.Get(1);
    }

    public Update GetUpdateFromMemoryCach()
    {
        if (_memoryCach.Get(2) is not null and Update)
        {
            return (Update)_memoryCach.Get(2);
        }

        else throw new MemoryCachException();
    }

    public int GetMessageIdFromMemoryCatch()
    {
        var obj = _memoryCach.Get(2);

        if (obj != null && obj is Update)
        {
            if ((obj as Update)?.CallbackQuery != null &&
                (obj as Update)?.CallbackQuery?.Message != null)
            {
                if ((obj as Update)?.CallbackQuery?.Message?.MessageId != null)
                {
                    return (obj as Update).CallbackQuery.Message.MessageId;
                }
                else throw new MemoryCachException();
            }


            if ((obj as Update)?.Message != null)
            {
                if ((obj as Update)?.Message?.MessageId != null)
                {
                    return (obj as Update).Message.MessageId;
                }
                else throw new MemoryCachException();
            }

            else throw new MemoryCachException();
        }

        else throw new MemoryCachException();
    }

    public string GetCallbackQueryIdFromMemoryCatch()
    {
        var obj = _memoryCach.Get(2);

        if (obj != null && obj is Update)
        {
            if ((obj as Update)?.CallbackQuery != null)
            {
                return (obj as Update)?.CallbackQuery?.Id;
            }

            else throw new MemoryCachException();
        }

        else throw new MemoryCachException();
    }

    public HerbDto GetHerbDtoFromMemoryCach()
    {
        if (_memoryCach.Get(4) is not null and HerbDto)
        {
            return (HerbDto)_memoryCach.Get(4);
        }

        else throw new MemoryCachException();
    }

    public HoneyDto GetHoneyDtoFromMemoryCach()
    {
        if (_memoryCach.Get(5) is not null and HoneyDto)
        {
            return (HoneyDto)_memoryCach.Get(5);
        }

        else throw new MemoryCachException();
    }

    public TeaDto GetTeaDtoFromMemoryCach()
    {
        if (_memoryCach.Get(3) is not null and TeaDto)
        {
            return (TeaDto)_memoryCach.Get(3);
        }

        else throw new MemoryCachException();
    }

    public void SetMemoryCach(string commandState, Update update)
    {
        _memoryCach.Set(1, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });

        _memoryCach.Set(2, update,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }

    public void SetMemoryCach(string commandState, TeaDto teaDto)
    {
        _memoryCach.Set(1, commandState,
            new MemoryCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
               });

        _memoryCach.Set(3, teaDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(TeaDto teaDto)
    {
        _memoryCach.Set(3, teaDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(string commandState, HerbDto herbDto)
    {
        _memoryCach.Set(1, commandState,
               new MemoryCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
               });

        _memoryCach.Set(4, herbDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(HerbDto herbDto)
    {
        _memoryCach.Set(4, herbDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(string commandState, Update update, HoneyDto honeyDto)
    {
        _memoryCach.Set(1, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

        _memoryCach.Set(2, update,
           new MemoryCacheEntryOptions
           {
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
           });

        _memoryCach.Set(5, honeyDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(string commandState, HoneyDto honeyDto)
    {
        _memoryCach.Set(1, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

        _memoryCach.Set(5, honeyDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(HoneyDto honeyDto)
    {
        _memoryCach.Set(5, honeyDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public void SetMemoryCach(long chatId, OrderDto orderDto)
    {
        _memoryCach.Set(chatId, orderDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }

    public OrderDto? GetOrderDtoFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId) is not null and OrderDto)
        {
            return (OrderDto)_memoryCach.Get(chatId);
        }

        else return null;
    }

    public void SetMemoryCach(long chatId, string commandState)
    {
        _memoryCach.Set(chatId + 1, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 1) is not null and string)
        {
            return (string)_memoryCach.Get(chatId + 1);
        }

        else return "_";
    }

    public void SetMemoryCach(long chatId, int messageId)
    {
        _memoryCach.Set(chatId + 2, messageId,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }

    public int GetMessageIdFromMemoryCatch(long chatId)
    {
        if (_memoryCach.Get(chatId + 2) is not null and int)
        {
            return (int)_memoryCach.Get(chatId + 2);
        }

        else throw new MemoryCachException();
    }

    public void SetMemoryCach(ProductDto productDto)
    {
        _memoryCach.Set(6, productDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
    }

    public ProductDto GetProductDtoFromMemoryCach()
    {
        if (_memoryCach.Get(3) is not null and TeaDto)
        {
            return (TeaDto)_memoryCach.Get(3);
        }

        if (_memoryCach.Get(4) is not null and HerbDto)
        {
            return (HerbDto)_memoryCach.Get(4);
        }

        if (_memoryCach.Get(5) is not null and HoneyDto)
        {
            return (HoneyDto)_memoryCach.Get(5);
        }

        else throw new MemoryCachException();
    }
}

