namespace QuanLiDichVuBien.Areas.clients.Models.ViewModels
{
	// ViewModel cho một Tour (dùng ở trang danh sách và sidebar)
	public class TourViewModel
	{
		public int TourId { get; set; }
		public string Title { get; set; } = "";
		public string Destination { get; set; } = "";
		public string Time { get; set; } = "";          // "3 ngày 2 đêm"
		public string Domain { get; set; } = "";         // "b" | "t" | "n"
		public decimal PriceAdult { get; set; }
		public decimal PriceChild { get; set; }
		public double Rating { get; set; }
		public List<string> Images { get; set; } = new();
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Quantity { get; set; }
	}

	// ViewModel cho trang chi tiết tour
	public class TourDetailViewModel : TourViewModel
	{
		public string Description { get; set; } = "";
		public List<TimelineViewModel> Timeline { get; set; } = new();
		public List<ReviewViewModel> Reviews { get; set; } = new();
		public double AverageStar { get; set; }
		public int ReviewCount { get; set; }
		public List<TourViewModel> RelatedTours { get; set; } = new();
		public bool CanReview { get; set; } = true;  // false = đã review rồi → ẩn form
	}

	// ViewModel cho lịch trình từng ngày
	public class TimelineViewModel
	{
		public int TimeLineId { get; set; }
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
	}

	// ViewModel cho từng đánh giá
	public class ReviewViewModel
	{
		public string UserName { get; set; } = "";
		public string Comment { get; set; } = "";
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	// ViewModel cho trang danh sách tours (ToursController)
	public class ToursPageViewModel
	{
		public List<TourViewModel> Tours { get; set; } = new();
		public List<TourViewModel> PopularTours { get; set; } = new();
		public DomainCountViewModel DomainsCount { get; set; } = new();
	}

	public class DomainCountViewModel
	{
		public int MienBac { get; set; }
		public int MienTrung { get; set; }
		public int MienNam { get; set; }
	}

	// ViewModel cho trang tour đã đặt
	public class TourBookedViewModel
	{
		public int TourId { get; set; }
		public string Title { get; set; } = "";
		public string FullName { get; set; } = "";
		public string Email { get; set; } = "";
		public string PhoneNumber { get; set; } = "";
		public string Address { get; set; } = "";
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int NumAdults { get; set; }
		public int NumChildren { get; set; }
		public decimal PriceAdult { get; set; }
		public decimal PriceChild { get; set; }
		public decimal TotalPrice { get; set; }
		public string PaymentMethod { get; set; } = "";  // "office-payment" | "paypal-payment" | "momo-payment"
		public string BookingStatus { get; set; } = "";  // "f" = finished, khác = active
		public int BookingId { get; set; }

		// Tính giảm giá để hiển thị trên view
		public decimal Discount =>
			(NumAdults * PriceAdult + NumChildren * PriceChild) - TotalPrice;

		// Ẩn nút hủy nếu còn < 7 ngày
		public bool HideCancelButton { get; set; }
	}
}