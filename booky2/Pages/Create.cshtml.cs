using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookyData;
using BookyData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace booky2.Pages
{
    public class CreateModel : PageModel
    {

        private readonly BookyContext _context;

        [TempData]
        public string LogMessage { get; set; }

        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public string FirstNameOfAuthor { get; set; }
        [BindProperty]
        public string LastNameOfAuthor { get; set; }

        public CreateModel(BookyContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if ((await _context.Authors.FirstOrDefaultAsync(author =>
                    author.LastName == LastNameOfAuthor && author.FirstName == FirstNameOfAuthor)) == null)
            {
                await _context.Authors.AddAsync(new Author()
                {
                    FirstName = FirstNameOfAuthor,
                    LastName = LastNameOfAuthor
                });
                await _context.SaveChangesAsync();
            }

            Book.Author = await _context.Authors.FirstOrDefaultAsync(author =>
                author.LastName == LastNameOfAuthor && author.FirstName == FirstNameOfAuthor);

            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();

            LogMessage = $"Book {Book.Title} added to the database.";

            return RedirectToPage("/Catalog");
        }
    }
}