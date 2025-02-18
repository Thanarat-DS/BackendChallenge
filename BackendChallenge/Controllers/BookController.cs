using BackendChallenge.Data;
using BackendChallenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BackendChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public BookController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // ดึงข้อมูลจาก IT Book Store API
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.itbook.store/1.0/search/mysql");
                var booksData = JObject.Parse(response);

                // Check ถ้าไม่เจอ Book
                var booksArray = booksData["books"];
                if (booksArray == null)
                {
                    return NotFound("No books found.");
                }
                var booksList = booksArray.ToObject<List<Book>>() ?? new List<Book>();
                var sortedBooks = booksList.OrderBy(b => b.Title).ToList();

                // Save to Database
                foreach (var book in sortedBooks)
                {
                    var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Isbn13 == book.Isbn13);
                    if (existingBook == null)
                    {
                        // ถ้าไม่พบหนังสือในฐานข้อมูล ให้เพิ่มหนังสือใหม่
                        await _context.Book.AddAsync(book);
                    }
                }
                await _context.SaveChangesAsync();
                var allBooks = await _context.Book.ToListAsync();

                return Ok(allBooks);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact the system admin.");
            }

        }
    }
}
