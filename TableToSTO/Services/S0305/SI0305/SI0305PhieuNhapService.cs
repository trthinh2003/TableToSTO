using TableToSTO.Models.DTO;
using TableToSTO.Models.M0305;

namespace TableToSTO.Services.S0305.SI0305
{
    public interface SI0305PhieuNhapService
    {
        Task<List<M0305DanhSachPhieuNhapSTO>> GetDSPhieuNhap();
        Task<List<HangHoaCbDTO>> GetHangHoaCbDTOs();
        Task CreatePhieuNhap(PhieuNhapDTO dto);
    }
}
