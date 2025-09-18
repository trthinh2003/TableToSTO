using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using TableToSTO.Models.DTO;
using TableToSTO.Models.Entities;
using TableToSTO.Models.M0305;
using TableToSTO.Services.S0305.SI0305;

namespace TableToSTO.Services.S0305
{
    public class S0305PhieuNhapService : SI0305PhieuNhapService
    {
        private readonly TableToSTOContext _context;
        private readonly ILogger<S0305PhieuNhapService> _logger;

        public S0305PhieuNhapService(
            TableToSTOContext context,
            ILogger<S0305PhieuNhapService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<M0305DanhSachPhieuNhapSTO>> GetDSPhieuNhap()
        {
            try
            {
                var data = await _context.Set<M0305DanhSachPhieuNhapSTO>()
                     .FromSqlRaw(@"EXEC S0305_DSPhieuNhap")
                     .AsNoTracking()
                     .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<HangHoaCbDTO>> GetHangHoaCbDTOs()
        {
            var data = await _context.DmHangHoas
                .Select(x => new HangHoaCbDTO
                {
                    IDHH = x.Id,
                    TenHangHoa = x.TenHangHoa ?? ""
                }).ToListAsync();
            return data;
        }

        public async Task CreatePhieuNhap(PhieuNhapDTO dto)
        {
            try
            {
                using (var conn = _context.Database.GetDbConnection() as SqlConnection)
                {
                    await conn.OpenAsync();

                    using (var cmd = new SqlCommand("dbo.S0305_CreatePhieuNhapDynamic", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SoPhieu", dto.SoPhieu);
                        cmd.Parameters.AddWithValue("@NguoiLap", dto.NguoiLap ?? "");
                        cmd.Parameters.AddWithValue("@NhaCungCap", dto.NhaCungCap ?? "");

                        cmd.Parameters.AddWithValue("@TypeDefinition", @$"");

                        // Tạo JSON từ chi tiết
                        string chiTietJson = System.Text.Json.JsonSerializer.Serialize(dto.ChiTiet);
                        _logger.LogInformation("Chi tiết JSON: {ChiTietJson}:", chiTietJson);
                        cmd.Parameters.AddWithValue("@ChiTietJson", chiTietJson);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi stored procedure tạo phiếu nhập");
                throw;
            }
        }
    }
}