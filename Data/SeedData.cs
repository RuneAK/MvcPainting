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

                var m = new Museum
                {
                    Name = "Metropolitan Museum of Art",
                    Country = "USA",
                    Director = "Max Hollein"
                };

                var a = new Artist
                {
                    Name = "Paul Gauguin",
                    Born = 1848,
                    Died = 1903,

                };

                context.Paintings.Add(
                    
                    new Painting
                    {
                        Title = "Still Life with Teapot and Fruit",
                        ImageUrl = "https://images.metmuseum.org/CRDImages/ep/original/DT1027.jpg",
                        Year = 1896,
                        Medium = "Oil on canvas",
                        Artist = a,
                        Museum = m
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
