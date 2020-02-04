using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCommon.Interfaces;
using WebApiCommonn.DataModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService _screenService;
        public ScreensController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet]
        public IEnumerable<Screen> GetScreens()
        {
            return _screenService.GetScreens();
        }

        [HttpGet("{id}")]
        public Screen GetScreen(int id)
        {
            return _screenService.GetScreen(id);
        }
        
        [HttpPost("{id}")]
        public IActionResult CreateScreen([FromBody]Screen screen)
        {
            _screenService.AddScreen(screen);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateScreen(int id, [FromBody]Screen screen)
        {
            _screenService.UpdateScreen(id, screen);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteScreen(int id)
        {
            _screenService.DeleteScreen(id);
            return Ok();
        }
    }
}