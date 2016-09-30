using OrderedListApi.Migrations;
using OrderedListApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OrderedListApi.DataAccess
{
    public class OrderedListDb : DbContext
    {
        public OrderedListDb() : base("OrderedListDb")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<OrderedListDb>());
        }

        public DbSet<OrderedListDetails> OrderedListDetails { get; set; }
        public DbSet<OrderedListItem> OrderedListItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OrderedListDetails>()
                .Property(x => x.ClientReferenceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OrderedListDb, Configuration>());
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}