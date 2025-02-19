using BackendChallenge.Data;
using BackendChallenge.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikeBook([FromBody] LikeRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Check User มีใน Database ไหม
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
            if (!userExists)
            {
                return NotFound("User not found.");
            }

            // Check Book มีใน Database ไหม
            var book = await _context.Book.FindAsync(request.BookId);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // Check ถ้า Like ไปแล้ว
            var existingLike = await _context.UserLike
                .FirstOrDefaultAsync(ul => ul.UserId == request.UserId && ul.BookId == request.BookId);

            if (existingLike != null)
            {
                return BadRequest("User has already liked this book.");
            }

            var userLike = new UserLike
            {
                UserId = request.UserId,
                BookId = request.BookId
            };

            // Save UserLike to Database
            _context.UserLike.Add(userLike);
            await _context.SaveChangesAsync();

            return Ok("Book liked successfully.");
        }
    }
}
