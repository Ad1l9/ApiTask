using ApiTask.Repositories.Interfaces;
using ApiTask.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            //IQueryable<Category> categories=await _repository.GetAllAsync(c=>c.Name,(page-1)*take,take,false);


            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            
            return StatusCode(StatusCodes.Status200OK,await _service.GetAysnc(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDTO categoryDto)
        {
            await _service.CreateAsync(categoryDto);

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
