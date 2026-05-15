using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.clients.Models.ViewModels;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class TourBookedController : Controller
	{
		// GET: /clients/TourBooked?bookingId=1&checkoutId=abc
		public IActionResult Index(int bookingId = 1, string? checkoutId = null)
		{
			var tourBooked = GetMockTourBooked(bookingId);

			// Ẩn nút "Hủy Tour" nếu ngày khởi hành còn dưới 7 ngày
			if (tourBooked.StartDate != default)
			{
				var diffDays = (tourBooked.StartDate - DateTime.Now).TotalDays;
				tourBooked.HideCancelButton = diffDays < 7;
			}

			ViewData["Title"] = "Tour đã đặt";
			return View(tourBooked);
		}

		// POST: /clients/TourBooked/CancelBooking
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CancelBooking(int tourId, int quantityAdults,
										   int quantityChildren, int bookingId)
		{
			// TODO: cập nhật DB – trả lại quantity, đổi trạng thái booking
			TempData["Success"] = "Hủy tour thành công!";
			return RedirectToAction("Index", "Home");
		}

		// -------------------------------------------------------------------
		// MOCK DATA – xóa / thay bằng service/repo khi có database
		// -------------------------------------------------------------------
		private TourBookedViewModel GetMockTourBooked(int bookingId)
		{
			return new TourBookedViewModel
			{
				BookingId = bookingId,
				TourId = 2,
				Title = "Đà Nẵng - Hội An - Huế",
				FullName = "Nguyễn Văn Anh",
				Email = "vanành@gmail.com",
				PhoneNumber = "0901234567",
				Address = "123 Nguyễn Trãi, Đà Nẵng",
				StartDate = DateTime.Now.AddDays(10),
				EndDate = DateTime.Now.AddDays(14),
				NumAdults = 2,
				NumChildren = 1,
				PriceAdult = 4_200_000,
				PriceChild = 2_500_000,
				TotalPrice = 9_800_000,   // sau khi giảm giá
				PaymentMethod = "office-payment",
				BookingStatus = "p"           // "f" = đã hoàn thành, khác = đang chờ
			};
		}
	}
}