using ApiTask.DTOs;
using ApiTask.DTOs.CategoryDTO;

namespace ApiTask.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page,int take);
        Task<GetCategoryDto> GetAysnc(int id);

        Task CreateAsync(CreateCategoryDTO createCategoryDTO);

        Task UpdateAsync(int id, UpdateCategoryDTO categoryDto);
        Task DeleteAsync(int id);
    }
}
