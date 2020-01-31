using ItLab.CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItLab.CrudApp.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly ItLab.CrudApp.Data.ItLabCrudAppContext _context;

        public CreateModel(ItLab.CrudApp.Data.ItLabCrudAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            AvailableTags = _context.Tags.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public int[] SelectedTags { get; set; }
        public IList<SelectListItem> AvailableTags { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            foreach (var tag in SelectedTags)
            {
                Product.ProductTags.Add(new ProductTag { TagId = tag });
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
