namespace TableToSTO.Models.M0305
{
    public class M0305DanhSachPhieuNhapSTO
    {
        public string? SoPhieu { get; set; }
        public long? IDHH { get; set; }
        public string? TenHangHoa { get; set; }
        public int SoLuongNhap { get; set; }
        public double DonGia { get; set; }
        public DateTime? NgayLapPhieu { get; set; } = DateTime.Now;
        public string? NhanVienLap { get; set; }
        public string? NhaCungCap { get; set; }
    }
}
