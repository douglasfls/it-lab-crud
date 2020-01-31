using ItLab.CrudApp.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItLab.CrudApp.Models
{
    public class Product
    {
        public Product()
        {
            ProductTags = new HashSet<ProductTag>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é requerido.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Preço é requerido")]
        [Display(Name = "Preço")]
        [RequiredGreaterThanZero(ErrorMessage = "O valor deve ser superior a 0.")]
        public decimal Price { get; set; }

        [Display(Name = "Data de Lançamento")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Categoria")]
        public CategoryType Category { get; set; }

        [Display(Name = "Tags")]
        public ICollection<ProductTag> ProductTags { get; set; }
    }


    public class ProductTag
    {
        public int Id { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }

    public enum CategoryType
    {
        Algum = 1,
        None = 0
    }
}
