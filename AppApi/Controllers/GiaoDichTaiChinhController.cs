using AppData;
using AppData.Entities;
using AppData.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoDichTaiChinhController : ControllerBase
    {
        private readonly IGiaoDichTaiChinhRepo _repo;
        private readonly IValidator<GiaoDichTaiChinh> _validator;
        private readonly AppDbContext _context;

        public GiaoDichTaiChinhController(IGiaoDichTaiChinhRepo repo, IValidator<GiaoDichTaiChinh> validator, AppDbContext context)
        {
            _repo = repo;
            _validator = validator;
            _context = context;
        }

        [HttpGet("tong-so-giao-dich")]
        public async Task<IActionResult> TinhToanTongSoGiaoDichCuaNguoiDung([FromQuery] string diaChiVi, [FromQuery] string loaiGiaoDich)
        {
            var tongSoLuongGiaoDich = await _context.GiaoDichTaiChinhs
                .Where(gd => gd.DiaChiVi == diaChiVi && gd.LoaiGiaoDich == loaiGiaoDich)
                .SumAsync(gd => gd.SoLuong);
            var tongChiPhiGiaoDich = await _context.GiaoDichTaiChinhs
                .Where(gd => gd.DiaChiVi == diaChiVi && gd.LoaiGiaoDich == loaiGiaoDich)
                .SumAsync(gd => gd.PhiGiaoDich);
            return Ok(new
            {
                DiaChiVi = diaChiVi,
                LoaiGiaoDich = loaiGiaoDich,
                TongSoLuongGiaoDich = tongSoLuongGiaoDich,
                TongChiPhiGiaoDich = tongChiPhiGiaoDich,
            }
            );

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var giaoDich = await _repo.GetAllAsync();
            return Ok(giaoDich);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var giaoDich = await _repo.GetByIdAsync(id);
            if (giaoDich == null) return NotFound();
            return Ok(giaoDich);    
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiaoDichTaiChinh giaoDich)
        {
            var validationResult = await _validator.ValidateAsync(giaoDich);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                } 
                ));
            }
            await _repo.AddAsync(giaoDich);
            return Ok(giaoDich);    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, GiaoDichTaiChinh giaoDich)
        {
            if(id != giaoDich.Id)
            {
                return BadRequest("Id Khong khop");
            }
            var validationResult = await _validator.ValidateAsync(giaoDich);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }
                ));
            }
            await _repo.UpdateAsync(giaoDich);
            return Ok(giaoDich);    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
