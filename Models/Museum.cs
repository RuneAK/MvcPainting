using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPainting.Models
{
    public class Museum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public virtual ICollection<Painting> Paintings { get; set; }
    }
}
