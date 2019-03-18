using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookyData;
using BookyData.Models;
using Microsoft.EntityFrameworkCore;

namespace BookyServices
{
    public class BookyAuthorService
    {
        private readonly BookyContext _context;

        public BookyAuthorService(BookyContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAsync(Author newAuthor)
        {
            await _context.Authors.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
            return await _context.Authors.FirstOrDefaultAsync(author =>
                author.LastName == newAuthor.LastName && author.FirstName == newAuthor.FirstName);
        }

        public async Task<Author> GetById(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToArrayAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            _context.Authors.Remove(await GetById(id));
            return id;
        }
    }
}