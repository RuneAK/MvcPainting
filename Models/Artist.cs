using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPainting.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Born { get; set; }
        public int Died { get; set; }
        public ICollection<Painting> Paintings { get; set; }

    }
}
