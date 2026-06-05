using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;

namespace AkhabarnaAPI.Services
{
    public class SourceService
    {
        private readonly SourceRepository _repo;
        private readonly ImageService imageService;

        public SourceService(SourceRepository repo, ImageService imageService)
        {
            _repo = repo;
            this.imageService = imageService;
        }

        public async Task<List<Source>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task Add(CreateSourceRequest dto)
        {
            string logoUrl = null;

            if (dto.Logo != null)
            {
                logoUrl = await imageService.UploadImageAsync(dto.Logo);
            }

            var source = new Source
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LogoUrl = logoUrl
            };

            await _repo.Add(source);
        }
    }
}
