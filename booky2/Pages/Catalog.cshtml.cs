using System;
using System.Collections;
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
    public class CatalogModel : PageModel
    {

        private readonly BookyBookService _bookService;

        [BindProperty]
        public IEnumerable<Book> Books { get; set; }

        public CatalogModel(BookyBookService bookService)
        {
            _bookService = bookService;
        }
        public void OnGet()
        {
            Books = _bookService.GetAll();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _bookService.RemoveAsync(id);
            return RedirectToPage();
        }

        public IActionResult OnPostEditAsync(int id)
        {
            return RedirectToPage("/Edit/" + id);
        }
    }
}