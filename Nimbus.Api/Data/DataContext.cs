using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nimbus.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<NimbusBox> tbNimbusData => Set<NimbusBox>();
        public DbSet<UserNimbus> tbAuthUserData => Set<UserNimbus>();
        public DbSet<NimbusUserUnion> tbNimbusUserData => Set<NimbusUserUnion>();
    }
}