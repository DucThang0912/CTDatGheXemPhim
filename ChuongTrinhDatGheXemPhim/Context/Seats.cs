namespace ChuongTrinhDatGheXemPhim.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Seats
    {
        [Key]
        [StringLength(20)]
        public string MaGhe { get; set; }

        [StringLength(50)]
        public string HangGhe { get; set; }

        public int? SoGhe { get; set; }

        public int? LoaiGhe { get; set; }

        public bool? TrangThai { get; set; }

        public virtual Categorys Categorys { get; set; }
    }
}
