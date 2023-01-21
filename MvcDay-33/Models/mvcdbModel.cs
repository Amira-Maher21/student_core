namespace MvcDay_33.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mvcdbModel : DbContext
    {
        public mvcdbModel()
            : base("name=mvcdbModel")
        {
        }

        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<department>()
                .HasMany(e => e.users)
                .WithOptional(e => e.department)
                .HasForeignKey(e => e.deptId);
        }
    }
}
