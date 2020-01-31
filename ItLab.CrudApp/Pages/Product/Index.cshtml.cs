using ItLab.CrudApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItLab.CrudApp.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly ItLab.CrudApp.Data.ItLabCrudAppContext _context;

        public IndexModel(ItLab.CrudApp.Data.ItLabCrudAppContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.Include(p => p.ProductTags).ThenInclude(p => p.Tag).ToListAsync();
        }
    }
}
