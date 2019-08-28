using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocetKsPaleta
{
    class Context: DbContext
    {
        public Context():base("name = CS")
        {
   
        }

        public DbSet<Adress> Adresses { get; set; }

    }
}
