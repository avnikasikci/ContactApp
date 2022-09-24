using ContactApp.Module.User.Application.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Persistence.Context
{

    public partial class PGDataUserContext : DbContext
    {


        public PGDataUserContext(DbContextOptions<PGDataUserContext> options)
      : base(options)
        {
            //Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<EntityUser> EntityUsers { get; set; }
        public DbSet<EntityUserContactInformation> EntityUserContactInformations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            //modelBuilder.UseIdentityColumns();
            modelBuilder.HasPostgresExtension("postgis")
             .HasPostgresExtension("postgis_topology")
             .HasPostgresExtension("tablefunc")
             .HasAnnotation("Relational:Collation", "en_US.UTF-8");
        }
    }
}
