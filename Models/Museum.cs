using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPainting.Models
{
    public class Museum
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public string Director { get; set; }
        public ICollection<Painting> Paintings { get; set; }
    }
}
