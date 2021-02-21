using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Web.Controllers
{
    [Route("api/receipe")]
    public class ReceipeController : ControllerBase
    {
        private readonly IReceipeAdder _receipeAdder;
        private readonly IReceipeGetter _receipeGetter;
        private readonly IReceipeUpdater _receipeUpdater;
        private readonly IReceipeRemover _receipeRemover;

        public ReceipeController(
            IReceipeAdder receipeAdder,
            IReceipeGetter receipeGetter,
            IReceipeUpdater receipeUpdater,
            IReceipeRemover receipeRemover)
        {
            _receipeAdder = receipeAdder;
            _receipeGetter = receipeGetter;
            _receipeUpdater = receipeUpdater;
            _receipeRemover = receipeRemover;
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
        public async Task<IActionResult> AddReceipe([FromForm] AddReceipeDto addReceipeDto)
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
