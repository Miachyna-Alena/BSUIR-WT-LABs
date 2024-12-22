using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miachyna.API.Data;
using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmeticsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CosmeticsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Cosmetics
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Cosmetic>>>> GetCosmetics(
            string? category,
            int pageNo = 1,
            int pageSize = 3)
        {
            var result = new ResponseData<ListModel<Cosmetic>>();

            var data = _context.Cosmetics
                .Include(x => x.Category)
                .Where(x => String.IsNullOrEmpty(category) || x.Category.NormalizedName.Equals(category));

            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);

            if (pageNo > totalPages)
                pageNo = totalPages;

            var listData = new ListModel<Cosmetic>()
            {
                Items = await data
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            result.Data = listData;

            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "There are no objects in the selected category :(";
            }

            return result;
        }

        // GET: api/Cosmetics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cosmetic>> GetCosmetic(int id)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);

            if (cosmetic == null)
            {
                return NotFound();
            }

            return cosmetic;
        }

        // PUT: api/Cosmetics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCosmetic(int id, Cosmetic cosmetic)
        {
            if (id != cosmetic.Id)
            {
                return BadRequest();
            }

            _context.Entry(cosmetic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CosmeticExists(id))
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

        // POST: api/Cosmetics
        [HttpPost]
        public async Task<ActionResult<Cosmetic>> PostCosmetic(Cosmetic cosmetic)
        {
            _context.Cosmetics.Add(cosmetic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCosmetic", new { id = cosmetic.Id }, cosmetic);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);

            if (cosmetic == null)
            {
                return NotFound();
            }

            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            var randomName = Path.GetRandomFileName();
            var extension = Path.GetExtension(image.FileName);
            var fileName = Path.ChangeExtension(randomName, extension);
            var filePath = Path.Combine(imagesPath, fileName);
            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);
            var host = "https://" + Request.Host;
            var url = $"{host}/Images/{fileName}";
            cosmetic.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Cosmetics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCosmetic(int id)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            _context.Cosmetics.Remove(cosmetic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CosmeticExists(int id)
        {
            return _context.Cosmetics.Any(e => e.Id == id);
        }
    }
}
