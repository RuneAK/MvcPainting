using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPainting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MvcPainting.Data
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcPaintingContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcPaintingContext>>()))
            {
                
                if (context.Paintings.Any())
                {
                    return;   // DB has been seeded
                }

                var m1 = new Museum
                {
                    Name = "Metropolitan Museum of Art",
                    Country = "USA",
                    Director = "Max Hollein"
                };

                var m2 = new Museum
                {
                    Name = "Ny Carlsberg Glyptotek",
                    Country = "Denmark",
                    Director = "Christine Buhl Andersen"
                };

                var a1= new Artist
                {
                    Name = "Paul Gauguin",
                    Born = 1848,
                    Died = 1903,

                };

                var a2 = new Artist
                {
                    Name = "Edouard Manet",
                    Born = 1832,
                    Died = 1883,

                };

                context.Paintings.Add(
                    new Painting
                    {
                        Title = "Still Life with Teapot and Fruit",
                        ImageUrl = "https://images.metmuseum.org/CRDImages/ep/original/DT1027.jpg",
                        Year = 1896,
                        Medium = "Oil on canvas",
                        Artist = a1,
                        Museum = m1
                    }
                );
                context.Add(
                    new Painting
                    {
                        Title = "Madame Manet at Bellevue",
                        ImageUrl = "https://images.metmuseum.org/CRDImages/ep/original/DT4224.jpg",
                        Year = 1880,
                        Medium = "Oil on canvas",
                        Artist = a2,
                        Museum = m1
                    }
                );
                context.Add(
                    new Painting
                    {
                        Title = "Study of a Nude",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Paul_Gauguin_001.jpg/800px-Paul_Gauguin_001.jpg",
                        Year = 1880,
                        Medium = "Oil on canvas",
                        Artist = a1,
                        Museum = m2
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
