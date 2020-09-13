using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: api/book
        // Not working.
        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Add(book);
                await _db.SaveChangesAsync();
                return CreatedAtAction("GetBooks", new { id = book.Id }, book);
            }
            else
            {
                return BadRequest();
            }

        }

        // GET: api/book
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        // GET: api/book/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _db.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Json(new { success=true,data = book });
        }

        // PUT: api/book/:id
        // Not working.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProducts(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _db.Entry(book).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/book/:id
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting." });
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }

        private bool BookExists(int id)
        {
            return _db.Book.Any(e => e.Id == id);
        }
    }
}