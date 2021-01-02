using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conamitary.Database
{
    public class ConamitaryContext: DbContext
    {
        public ConamitaryContext(DbContextOptions<ConamitaryContext> contextOptions)
            : base(contextOptions)
        { }

        public DbSet<Receipe> Receips { get; set; }
    }
}
