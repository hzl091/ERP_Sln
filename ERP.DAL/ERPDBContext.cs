using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ERP.Domain;

namespace ERP.DAL
{
    public class ERPDBContext : DbContext
    {
        static ERPDBContext()
        {
            //Database.SetInitializer<ERPDBContext>(null);
        }
        public ERPDBContext()
            : base("name=erpcon") 
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DatabaseGeneratedAttributeConvention>();

            modelBuilder.Entity<TableInfo>()
                .HasKey(t =>t.Id)
                .ToTable("tab_info")
                .HasMany<ColumnInfo>(c => c.ColumnInfos)
                .WithRequired(c => c.TableInfo)
                .HasForeignKey(c =>c.TableInfoId);

            modelBuilder.Entity<ColumnInfo>()
                .HasKey(c =>c.Id)
                .ToTable("tab_col_info");

            modelBuilder.Entity<PreCustomer>()
                .HasKey(c => c.ID)
                .ToTable("pre_customers");
        }

        public DbSet<TableInfo> TableInfos { get; set; }

        public DbSet<PreCustomer> PreCustomers { get; set; }
    }
}
