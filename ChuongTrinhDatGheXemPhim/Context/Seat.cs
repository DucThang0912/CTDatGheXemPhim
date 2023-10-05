namespace ChuongTrinhDatGheXemPhim.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Seat
    {
        [Key]
        public int MaGhe { get; set; }

        [StringLength(50)]
        public string TenGhe { get; set; }

        public int? HangGhe { get; set; }

        public int? SoGhe { get; set; }

        public int? LoaiGhe { get; set; }

        public bool? TrangThai { get; set; }

        public virtual Category Category { get; set; }
    }
}
