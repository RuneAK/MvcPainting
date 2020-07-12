using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcPainting.Models;

namespace MvcPainting.Data
{
    public class MvcPaintingContext : DbContext
    {
        public MvcPaintingContext(DbContextOptions<MvcPaintingContext> options)
            : base(options)
        {
        }

        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Museum> Museums { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }

}