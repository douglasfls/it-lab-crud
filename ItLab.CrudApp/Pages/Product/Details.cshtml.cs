using ItLab.CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ItLab.CrudApp.ProductPage
{
    public class DetailsModel : PageModel
    {
        private readonly ItLab.CrudApp.Data.ItLabCrudAppContext _context;

        public DetailsModel(ItLab.CrudApp.Data.ItLabCrudAppContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
