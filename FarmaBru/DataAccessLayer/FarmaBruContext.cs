using MetaData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FarmaBruContext : DbContext
    {
        public FarmaBruContext(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;
                                                    AttachDbFilename=C:\Users\Henrique\Documents\FarmaBruDB.mdf;
                                                    Integrated Security=True;Connect Timeout=5"
                                , x => x.EnableRetryOnFailure(3));
        }
        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

    }
}
