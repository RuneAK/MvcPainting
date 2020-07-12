using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPainting.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Born { get; set; }
        public int Died { get; set; }
        public virtual ICollection<Painting> Paintings { get; set; }

    }
}
