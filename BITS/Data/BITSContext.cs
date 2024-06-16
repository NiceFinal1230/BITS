using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BITS.Models;

namespace BITS.Data
{
    public class BITSContext : DbContext
    {
        public BITSContext (DbContextOptions<BITSContext> options)
            : base(options)
        {
        }
        public DbSet<BITS.Models.Source> Source { get; set; } = default!;
        public DbSet<BITS.Models.Product> Product { get; set; } = default!;
        public DbSet<BITS.Models.Cart> Cart { get; set; } = default!;
        public DbSet<BITS.Models.Stocktake> Stocktake { get; set; } = default!;
        public DbSet<BITS.Models.Discount> Discount { get; set; } = default!;
    }
}
