using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class MovieViewModel
    {
        #region FromModel
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20, ErrorMessage = "Stock value should be between 1 and 20")]
        public int? NumberInStock { get; set; }

        // Foreign key
        [Display(Name = nameof(Genre))]
        public int GenreId { get; set; }

        // Navigation property
        public Genre Genre { get; set; }
        #endregion FromModel

        public IEnumerable<Genre> Genres { get; set; }
    }
}