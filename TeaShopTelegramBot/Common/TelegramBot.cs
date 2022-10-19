using Microsoft.Extensions.Configuration;

namespace TeaShopTelegramBot.Common;

public class TelegramBot
{
    private readonly IConfiguration _configuration;
    private TelegramBotClient? _botClient;

    public TelegramBot(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TelegramBotClient> GetBot()
    {
        if (_botClient != null)
        {
            return _botClient;
        }

        _botClient = new TelegramBotClient(_configuration.GetValue<string>("Token"));

        var hook = $"{_configuration.GetValue<string>("Url")}api/message/update";
        await _botClient.SetWebhookAsync(hook);

        var me = _botClient.GetMeAsync().Result;
        Console.WriteLine($"Начал принимать обновления из чатов с ботом @{me.Username}");

        return _botClient;
    }
}

