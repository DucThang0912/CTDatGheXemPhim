using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChuongTrinhDatGheXemPhim.Context
{
    public partial class ModelSeat : DbContext
    {
        public ModelSeat()
            : base("name=Model12")
        {
        }

        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Seats)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.LoaiGhe);
        }
    }
}
