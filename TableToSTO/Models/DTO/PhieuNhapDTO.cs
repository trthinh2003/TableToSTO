namespace TableToSTO.Models.DTO
{
    public class PhieuNhapDTO
    {
        public string SoPhieu { get; set; }
        public string NguoiLap { get; set; }
        public string NhaCungCap { get; set; }
        public List<PhieuNhapChiTietDTO> ChiTiet { get; set; } = new();
    }

    public class PhieuNhapChiTietDTO
    {
        public long IDHH { get; set; }
        public string TenHangHoa { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
    }
}
