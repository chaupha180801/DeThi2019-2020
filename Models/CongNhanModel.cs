using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DETHI20.Models
{
    public class CongNhanModel
    {
        private string macongnhan;
        private string tencongnhan;
        private int gioitinh;
        private int namsinh;
        private string nuocve;
        private string madiemcachly;

        public string MaCongNhan { get; set; }
        public string TenCongNhan { get; set; }
        public int GioiTinh { get; set; }
        public int NamSinh { get; set; }
        public string NuocVe { get; set; }
        public string MaDiemCachLy { get; set; }

    }
}
