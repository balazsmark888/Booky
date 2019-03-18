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
        private readonly BookyContext _context;

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

        public async void ModifyAsync(Book newBook)
        {
            //if ((await _context.Authors.FirstOrDefaultAsync(author =>
            //        author.LastName == newBook.Author.LastName && author.FirstName == newBook.Author.LastName)) == null)
            //{
            //    await _context.Authors.AddAsync(new Author()
            //    {
            //        LastName = newBook.Author.LastName,
            //        FirstName = newBook.Author.FirstName
            //    });
            //    await _context.SaveChangesAsync();
            //}

            //newBook.Author.Id = (await _context.Authors.FirstOrDefaultAsync(author =>
            //    author.LastName == newBook.Author.LastName && author.FirstName == newBook.Author.LastName)).Id;

            _context.Attach(newBook).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                throw new Exception($"The book {newBook.Title} could not be updated!", dbUpdateConcurrencyException);
            }
        }
    }
}
