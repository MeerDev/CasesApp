using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CasesApp.Models
{
    public class CasesAppContext : DbContext
    {
        public CasesAppContext (DbContextOptions<CasesAppContext> options)
            : base(options)
        {
        }

        public DbSet<CasesApp.Models.Case> Case { get; set; }
    }
}
