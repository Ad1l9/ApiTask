using ApiTask.DTOs.TagDTO;

namespace ApiTask.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDTO>> GetAllAsync(int page, int take);
        Task<GetTagDTO> GetAysnc(int id);

        Task CreateAsync(CreateTagDTO createTagDTO);

        Task UpdateAsync(int id, UpdateTagDTO updateTagDTO);
        Task DeleteAsync(int id);
    }
}
