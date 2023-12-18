using ApiTask.DTOs.CategoryDTO;
using ApiTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ApiTask.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take,isTracking:false).ToListAsync();

            ICollection<GetCategoryDto> categoryDtos = new List<GetCategoryDto>();

            foreach (Category category in categories)
            {
                categoryDtos.Add(new() { Id=category.Id,Name=category.Name});
            }
            return categoryDtos;
        }

        public async Task<GetCategoryDto> GetAysnc(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            return new GetCategoryDto() { Id = category.Id, Name = category.Name };
        }

        public async Task CreateAsync(CreateCategoryDTO createCategoryDTO)
        {
            await _repository.AddAsync(new Category()
            {
                Name = createCategoryDTO.Name,
            });
            
        }

        public async Task UpdateAsync(int id, UpdateCategoryDTO categoryDto)
        {
            Category category =await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");

            category.Name = categoryDto.Name;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}
