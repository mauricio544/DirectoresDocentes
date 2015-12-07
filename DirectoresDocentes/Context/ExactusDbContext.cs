using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DirectoresDocentes.Context
{
    public class ExactusDbContext : DbContext
    {
        public ExactusDbContext()
            : base("DefaultConnection")
        {

        }
    }
}