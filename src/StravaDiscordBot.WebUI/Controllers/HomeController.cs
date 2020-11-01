using Microsoft.AspNetCore.Mvc;

namespace StravaDiscordBot.WebUI.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Thank you mario! But our princess is in another castle!");
        }
    }
}