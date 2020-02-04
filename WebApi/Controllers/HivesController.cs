using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCommon.DataModel;
using WebApiCommon.Interfaces.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HivesController : ControllerBase
    {
        private readonly IHiveService _hiveService;
        public HivesController(IHiveService hiveService)
        {
            _hiveService = hiveService;
        }

        [HttpGet]
        public IEnumerable<Hive> GetHives()
        {
            return _hiveService.GetHives();
        }

        [HttpGet("{Id}")]
        public Hive GetHive(int id)
        {
            return _hiveService.GetHive(id);
        }

        
        [HttpPost("{Id}")]
        public IActionResult CreateHive([FromBody]Hive hive)
        {
            _hiveService.AddHive(hive);
            return Ok();
        }

        
        [HttpPut("{Id}")]
        public IActionResult UpdateHive(int id, [FromBody]Hive hive)
        {
            _hiveService.UpdateHiveAddress(id, hive);
            return Ok();
        }

        
        [HttpDelete("{Id}")]
        public IActionResult DeleteHive(int id)
        {
            _hiveService.DeleteHive(id);
            return Ok();
        }
    }
}
