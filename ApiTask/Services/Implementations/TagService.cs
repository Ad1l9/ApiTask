using ApiTask.DTOs.CategoryDTO;
using ApiTask.DTOs.TagDTO;
using ApiTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Services.Implementations
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetTagDTO>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();

            ICollection<GetTagDTO> tagDTOs = new List<GetTagDTO>();

            foreach (Tag tag in tags)
            {
                tagDTOs.Add(new() { Id = tag.Id, Name = tag.Name });
            }
            return tagDTOs;
        }

        public async Task<GetTagDTO> GetAysnc(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            return new GetTagDTO() { Id = tag.Id, Name = tag.Name };
        }

        public async Task CreateAsync(CreateTagDTO createTagDTO)
        {
            await _repository.AddAsync(new Tag()
            {
                Name = createTagDTO.Name,
            });

        }

        public async Task UpdateAsync(int id, UpdateTagDTO tagDTO)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");

            tag.Name = tagDTO.Name;
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }
    }
}
