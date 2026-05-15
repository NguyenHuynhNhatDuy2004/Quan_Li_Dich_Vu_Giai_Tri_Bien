// Areas/admin/Models/CouponViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public enum DiscountType
	{
		Percent,  // Giảm theo %
		Amount    // Giảm theo số tiền
	}

	public class CouponViewModel
	{
		public Guid CouponID { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mã giảm giá")]
		[Display(Name = "Mã giảm giá")]
		[StringLength(20, MinimumLength = 4, ErrorMessage = "Mã từ 4-20 ký tự")]
		public string Code { get; set; }

		[Display(Name = "Mô tả")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn loại giảm giá")]
		[Display(Name = "Loại giảm giá")]
		public DiscountType DiscountType { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập giá trị giảm")]
		[Display(Name = "Giá trị giảm")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn 0")]
		public decimal DiscountValue { get; set; }

		[Display(Name = "Giảm tối đa (₫)")]
		public decimal? MaxDiscountAmount { get; set; } // Chỉ dùng khi DiscountType = Percent

		[Display(Name = "Đơn tối thiểu (₫)")]
		public decimal MinOrderAmount { get; set; } = 0;

		[Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
		[Display(Name = "Ngày bắt đầu")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
		[Display(Name = "Ngày kết thúc")]
		public DateTime EndDate { get; set; }

		[Display(Name = "Số lượng")]
		[Range(1, 10000)]
		public int Quantity { get; set; } = 1;

		[Display(Name = "Đã dùng")]
		public int UsedCount { get; set; } = 0;

		[Display(Name = "Kích hoạt")]
		public bool IsActive { get; set; } = true;

		public DateTime CreateAt { get; set; }
		public DateTime ModifyAt { get; set; }

		// Computed
		public bool IsExpired => DateTime.Now > EndDate;
		public bool IsValid => IsActive && !IsExpired && UsedCount < Quantity;
		public int RemainingQuantity => Quantity - UsedCount;
	}
}