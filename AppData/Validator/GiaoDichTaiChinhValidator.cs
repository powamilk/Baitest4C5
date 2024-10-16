using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using FluentValidation;

namespace AppData.Validator
{
    public class GiaoDichTaiChinhValidator : AbstractValidator<GiaoDichTaiChinh>
    {
        public GiaoDichTaiChinhValidator()
        {
            RuleFor(g => g.NguoiThucHien)
                .NotEmpty().WithMessage("Ten nguoi thuc hien ko duoc bor trong");
            RuleFor(g => g.DiaChiVi)
                .NotEmpty().WithMessage("Dia chi vi khong duoc bo trong");
            RuleFor(g => g.LoaiGiaoDich)
                .NotEmpty().WithMessage("Loại Giao Dịch khong duoc bo trong")
                .Must(ValidatorLoaiGiaoDich).WithMessage("trạng thái phải là Gửi, Nhận, Hoán đổi");
            RuleFor(g => g.SoLuong)
                .GreaterThan(0).WithMessage("Số Lượng phải lớn hơn 0");
            RuleFor(g => g.NgayGiaoDich)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày Giao dich Khong dược tron tương lai");
            RuleFor(g => g.PhiGiaoDich)
                .GreaterThan(0).WithMessage("Phí giao dịch phải lớn hơn 0");
            RuleFor(g => g.TrangThai)
                .NotEmpty().WithMessage("Trạng Thái khong duoc bo trong")
                .Must(ValidatorTrangThai).WithMessage("trạng thái phải là Đang xử lý, Thành công, Thất bại");
        }
        private bool ValidatorLoaiGiaoDich(string LoaiGiaoDich)
        {
            var validTrangThai = new[] { "Gửi", "Nhận", "Hoán Đổi" };
            return Array.Exists(validTrangThai, s => s.Equals(LoaiGiaoDich, StringComparison.OrdinalIgnoreCase));
        }

        private bool ValidatorTrangThai(string TrangThai)
        {
            var validTrangThai = new[] { "Đang xử lý", "Thành công", "Thất Bại" };
            return Array.Exists(validTrangThai, s => s.Equals(TrangThai,StringComparison.OrdinalIgnoreCase));
        }
           
    }
}
