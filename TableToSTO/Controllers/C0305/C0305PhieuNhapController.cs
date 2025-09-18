using Microsoft.AspNetCore.Mvc;
using TableToSTO.Models.DTO;
using TableToSTO.Services.S0305.SI0305;

namespace TableToSTO.Controllers.C0305
{
    public class C0305PhieuNhapController : Controller
    {
        private readonly ILogger<C0305PhieuNhapController> _logger;
        private readonly SI0305PhieuNhapService _phieuNhapService;
        public C0305PhieuNhapController(ILogger<C0305PhieuNhapController> logger, SI0305PhieuNhapService phieuNhapService)
        {
            _logger = logger;
            _phieuNhapService = phieuNhapService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _phieuNhapService.GetDSPhieuNhap();
                return View("~/Views/V0305/V0305DSPhieuNhap/Index.cshtml", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Có lỗi trong phương thức Index");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult Create() 
        {
            ViewBag.HangHoaCbDTOs = _phieuNhapService.GetHangHoaCbDTOs().Result;
            return View("~/Views/V0305/V0305DSPhieuNhap/Create.cshtml"); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhieuNhapDTO phieuNhapDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _phieuNhapService.CreatePhieuNhap(phieuNhapDTO);

                return Ok(new { success = true, message = "Tạo phiếu nhập thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Có lỗi khi tạo phiếu nhập");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
