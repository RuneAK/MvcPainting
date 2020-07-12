using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcPainting.Models
{
    public class Painting
    {
        public int Id { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Medium { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Museum Museum { get; set; }
    }
}
