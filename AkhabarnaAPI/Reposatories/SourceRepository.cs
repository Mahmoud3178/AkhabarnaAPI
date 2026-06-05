using AkhabarnaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Reposatories
{
    public class SourceRepository
    {
        private readonly AppDbContext _context;

        public SourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Source>> GetAll()
        {
            return await _context.Sources.ToListAsync();
        }

        public async Task<Source> GetById(Guid id)
        {
            return await _context.Sources.FindAsync(id);
        }

        public async Task Add(Source source)
        {
            _context.Sources.Add(source);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var source = await _context.Sources.FindAsync(id);
            if (source != null)
            {
                _context.Sources.Remove(source);
                await _context.SaveChangesAsync();
            }
        }
    }
}
