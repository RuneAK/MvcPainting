using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPainting.Models
{
    public class PaintingViewModel
    {
        
        public int Id { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Medium { get; set; }
        //public Painting Painting { get; set; }
        public List<Artist> Artists { get; set; }
        //public SelectList ArtistSL { get; set; }
        public int SelectedArtistId { get; set; }
        public List<Museum> Museums { get; set; }
        //public SelectList MuseumSL { get; set; }
        public int SelectedMuseumId { get; set; }
    }
}
