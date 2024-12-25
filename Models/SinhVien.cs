namespace De01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [Key]
        [StringLength(6)]
        public string MaSv { get; set; }

        [StringLength(40)]
        public string HoTenSV { get; set; }

        [StringLength(3)]
        public string MaLop { get; set; }

        public DateTime? NgaySinh { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
