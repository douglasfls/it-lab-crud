using ItLab.CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItLab.CrudApp.ProductPage
{
    public class EditModel : PageModel
    {
        private readonly Data.ItLabCrudAppContext _context;

        public EditModel(Data.ItLabCrudAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public int[] SelectedTags { get; set; }
        public IList<SelectListItem> AvailableTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.Include(p => p.ProductTags).FirstOrDefaultAsync(m => m.Id == id);

            var existingTags = Product.ProductTags.Select(p => p.TagId);

            AvailableTags = _context.Tags.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = existingTags.Contains(p.Id)
            }).ToList();

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RemoveRange(_context.ProductTag.Where(p => p.ProductId == Product.Id));

            foreach (var tag in SelectedTags)
            {
                Product.ProductTags.Add(new ProductTag { TagId = tag });
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
