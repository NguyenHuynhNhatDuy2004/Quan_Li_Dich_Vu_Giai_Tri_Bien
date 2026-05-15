using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class PaymentViewModel
	{
		[Display(Name = "Mã thanh toán")]
		public Guid PaymentID { get; set; }

		[Required(ErrorMessage = "Mã đặt chỗ không được để trống")]
		[Display(Name = "Mã đặt chỗ (Booking)")]
		public Guid BookingID { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
		[Display(Name = "Phương thức")]
		public string Method { get; set; }

		[Display(Name = "Ngày tạo")]
		public DateTime CreateAt { get; set; }

		[Display(Name = "Ngày cập nhật")]
		public DateTime ModifyAt { get; set; }

		[Required(ErrorMessage = "Số tiền không được để trống")]
		[Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0")]
		[Display(Name = "Tổng tiền")]
		public decimal Total { get; set; }

		[Display(Name = "Trạng thái")]
		public bool Status { get; set; } // true: Completed, false: Pending
	}
}