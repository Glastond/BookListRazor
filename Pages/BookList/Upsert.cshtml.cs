using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel (ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]

        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)  // The get method gets the Id passed from the Page.
        {
            Book = new Book();
            if(id == null)
            {
                // Create
                return Page();
            }

            Book = await _db.Book.FindAsync(id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    // Create.
                    _db.Book.Add(Book);
                }else
                {
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}