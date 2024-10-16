using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class GiaoDichTaiChinh
    {
        public Guid Id { get; set; } 
        public string NguoiThucHien { get; set; } 
        public string LoaiGiaoDich {  get; set; }   
        public string DiaChiVi { get; set; } 
        public decimal SoLuong { get; set; }
        public DateTime NgayGiaoDich { get; set; }  
        public decimal PhiGiaoDich { get; set; }    
        public string TrangThai {  get; set; } 

    }
}
