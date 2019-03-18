using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookyData;
using BookyData.Models;
using BookyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace booky2.Pages
{
    public class EditModel : PageModel
    {
        private readonly BookyBookService _bookService;

        [TempData]
        public string LogMessage { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public EditModel(BookyBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }
            Book = await _bookService.GetByIdAsync((int)id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
       
            await _bookService.UpdateAsync(Book);

            LogMessage = $"Book {Book.Title} modified succesfully.";

            return RedirectToPage("/Catalog");
        }
    }
}