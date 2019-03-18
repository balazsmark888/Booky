using BookyData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookyData.Models;
using Microsoft.EntityFrameworkCore;

namespace BookyServices
{
    public class BookyBookService
    {
        private BookyContext _context;

        public BookyBookService(BookyContext context)
        {
            _context = context;
        }

        public void Add(Book newBook)
        {
            _context.Add(newBook);
            _context.SaveChanges();
        }

        public async Task<Book> AddAsync(Book newBook)
        {
            await _context.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public void Remove(int id)
        {
            _context.Books.Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task<int> RemoveAsync(int id)
        {
            _context.Books.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
            return id;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Include(book => book.Author);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(book => book.Author)
                .ToListAsync();
        }

        public Book GetById(int id)
        {
            return _context.Books.Include(book => book.Author)
                .FirstOrDefault(book => book.Id == id);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.Include(book => book.Author)
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        public Author GetAuthor(int id)
        {
            return GetByIdAsync(id).Result.Author;
            //return GetById(id).Author;
        }

        public string GetGenre(int id)
        {
            return GetByIdAsync(id).Result.Genre;
        }

        public string GetTitle(int id)
        {
            return GetByIdAsync(id).Result.Title;
        }

        public int GetPrice(int id)
        {
            return GetByIdAsync(id).Result.Price;
        }
    }
}
