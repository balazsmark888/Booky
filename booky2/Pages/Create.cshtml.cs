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

        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }

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
                    author.LastName == LastName && author.FirstName == FirstName)) == null)
            {
                await _context.Authors.AddAsync(new Author()
                {
                    FirstName = FirstName,
                    LastName = LastName
                });
                await _context.SaveChangesAsync();
            }

            Book.Author = await _context.Authors.FirstOrDefaultAsync(author =>
                author.LastName == LastName && author.FirstName == FirstName);

            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Catalog");
        }
    }
}