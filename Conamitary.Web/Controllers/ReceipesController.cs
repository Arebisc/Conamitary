using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Conamitary.Web.Controllers
{
    [Route("api/receipes")]
    public class ReceipesController : ControllerBase
    {
        private readonly IReceipeAdder _receipeAdder;
        private readonly IReceipeGetter _receipeGetter;
        private readonly IReceipeUpdater _receipeUpdater;
        private readonly IReceipeDeleter _receipeRemover;
        private readonly ILogger<ReceipesController> _logger;

        public ReceipesController(
            IReceipeAdder receipeAdder,
            IReceipeGetter receipeGetter,
            IReceipeUpdater receipeUpdater,
            IReceipeDeleter receipeRemover,
            ILogger<ReceipesController> logger)
        {
            _receipeAdder = receipeAdder;
            _receipeGetter = receipeGetter;
            _receipeUpdater = receipeUpdater;
            _receipeRemover = receipeRemover;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetReceipes()
        {
            var receipe = await _receipeGetter.Get();
            return Ok(receipe);
        }

        [HttpGet("{receipeId}")]
        public async Task<IActionResult> GetReceipes(Guid receipeId)
        {
            var receipes = await _receipeGetter.Get(receipeId);
            return Ok(receipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddReceipe([FromBody] AddReceipeDto addReceipeDto)
        {
            var addedDto = await _receipeAdder.Add(addReceipeDto);
            return Ok(addedDto);
        }

        [HttpPut("{receipeId}")]
        public async Task<IActionResult> UpdateReceipe(Guid receipeId, [FromBody] ReceipeDto receipeDto)
        {
            var updatedDto = await _receipeUpdater.Update(receipeId, receipeDto);
            return Ok(updatedDto);
        }

        [HttpDelete("{receipeId}")]
        public async Task<IActionResult> RemoveReceipe(Guid receipeId)
        {
            var removedDto = await _receipeRemover.Remove(receipeId);
            return Ok(removedDto);
        }
    }
}
