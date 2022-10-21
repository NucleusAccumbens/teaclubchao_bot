﻿namespace TeaShopTelegramBot.Common.Interfaces;

public interface ICommandAnalyzer
{
    Task AnalyzeCommandsAsync(ITelegramBotClient botClient, Update update);
}

