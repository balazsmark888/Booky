using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookyData;
using BookyData.Models;
using BookyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace booky2.Pages
{
    public class EditModel : PageModel
    {
        private readonly BookyBookService _bookService;

        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public string FirstNameOfAuthor { get; set; }
        [BindProperty]
        public string LastNameOfAuthor { get; set; }

        public EditModel(BookyBookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet(int id)
        {
            Book = _bookService.GetById(id);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookService.GetByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Catalog");
            }
            _bookService.ModifyAsync(Book);
            return RedirectToPage("/Catalog");
        }
    }
}