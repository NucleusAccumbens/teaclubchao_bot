using Microsoft.AspNetCore.Mvc;
using TeaShopTelegramBot.Common;
using TeaShopTelegramBot.Common.Interfaces;
using Telegram.Bot.Types;

namespace TeaShopBotWeb.Controllers.TelegramBotControllers;

[ApiController]
[Route("api/message/update")]
public class TelegramBotController : ControllerBase
{
    private readonly ICommandAnalyzer _commandAnalyzer;
    private readonly TelegramBot _bot;

    public TelegramBotController(ICommandAnalyzer commandAnalyzer, TelegramBot bot)
    {
        _commandAnalyzer = commandAnalyzer;
        _bot = bot;
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody]Update update)
    {
        try
        {
            await _commandAnalyzer.AnalyzeCommandsAsync(await _bot.GetBot(), 
                update);
        }
        catch (Exception)
        {
            return Ok();
        }

        return Ok();
    }
}
