using DuAn_DTNC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAn_DTNC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly DTNCDbContext _context;

        public FoodController(DTNCDbContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItems()
        {
            return await _context.FoodItems.ToListAsync();
        }

        // GET: api/Food/5

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItem>> GetFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // GET: api/Food/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FoodItem>>> SearchFoodItems(string keyword)
        {
            var query = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(fi =>
                    fi.Name.Contains(keyword) ||
                    fi.Description.Contains(keyword) ||
                    fi.Category.Contains(keyword)
                );
            }

            return await query.ToListAsync();
        }

        // GET: api/Food/combo-search
        [HttpGet("combo-search")]
        public async Task<ActionResult<IEnumerable<Combo>>> SearchCombos(string keyword)
        {
            var query = _context.Combos.Include(c => c.FoodItems).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c =>
                    c.Name.Contains(keyword) ||
                    c.Description.Contains(keyword) ||
                    c.FoodItems.Any(fi =>
                        fi.Name.Contains(keyword) ||
                        fi.Description.Contains(keyword) ||
                        fi.Category.Contains(keyword)
                    )
                );
            }

            return await query.ToListAsync();
        }
    }
}
