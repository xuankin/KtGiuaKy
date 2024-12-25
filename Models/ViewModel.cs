using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De01.Models
{
    public class ViewModel
    {
        public string MaSv { get; set; }

        [StringLength(40)]
        public string HoTenSV { get; set; }

        
        [StringLength(30)]
        public string TenLop { get; set; }

        public DateTime? NgaySinh { get; set; }

    }
}
