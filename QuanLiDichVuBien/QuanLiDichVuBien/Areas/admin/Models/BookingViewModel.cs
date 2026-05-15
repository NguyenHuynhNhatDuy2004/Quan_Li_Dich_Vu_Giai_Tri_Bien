using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class BookingViewModel
	{
		public Guid BookingID { get; set; }
		[Display(Name = "Khách hàng")]
		public Guid CustomerID { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerPhone { get; set; }  // Thêm
		public string? CustomerEmail { get; set; }  // Thêm
		[Display(Name = "Tour du lịch")]
		public Guid TourID { get; set; }
		public string? TourTitle { get; set; }
		[Display(Name = "Người lớn")]
		[Range(1, 100)]
		public int Adult { get; set; }
		[Display(Name = "Trẻ em")]
		[Range(0, 100)]
		public int Child { get; set; }
		[Display(Name = "Tổng tiền")]
		public decimal TotalPrice { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime ModifyAt { get; set; }
		public bool PaymentStatus { get; set; }
	}
}