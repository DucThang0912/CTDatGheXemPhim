using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChuongTrinhDatGheXemPhim.Context
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorys>()
                .HasMany(e => e.Seats)
                .WithOptional(e => e.Categorys)
                .HasForeignKey(e => e.LoaiGhe);
        }
    }
}
