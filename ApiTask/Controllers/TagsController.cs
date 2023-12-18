using ApiTask.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=2)
        {

            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            return StatusCode(StatusCodes.Status200OK, await _service.GetAysnc(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTagDTO tagDto)
        {
            await _service.CreateAsync(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.UpdateAsync(id, new() { Name = name });

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.DeleteAsync(id);

            return NoContent();
        }


    }
}
