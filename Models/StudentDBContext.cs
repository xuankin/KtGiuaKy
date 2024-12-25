using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace De01.Models
{
    public partial class StudentDBContext : DbContext
    {
        public StudentDBContext()
            : base("name=StudentDBContext")
        {
        }

        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lop>()
                .Property(e => e.MaLop)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaSv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaLop)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
