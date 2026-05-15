using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class MyTourController : Controller
	{
		public IActionResult Index()
		{
			// Tạo dữ liệu giả để test giao diện
			var myTours = new List<dynamic>
			{
				new {
					bookingStatus = "b",
					destination = "Sơn Trà, Đà Nẵng",
					rating = 5,
					title = "Lặn ngắm san hô Bán đảo Sơn Trà",
					description = "<p>Khám phá vẻ đẹp kỳ thú dưới lòng đại dương tại hòn Sụp.</p>",
					time = "4 Giờ",
					numAdults = 2,
					numChildren = 0,
					totalPrice = 1500000,
					tourId = 1,
					bookingId = 101,
					checkoutId = 201,
					images = new string[] { "tour1.jpg" }
				},
				new {
					bookingStatus = "y",
					destination = "Mỹ Khê, Đà Nẵng",
					rating = 4,
					title = "Bay dù lượn trên biển Mỹ Khê",
					description = "Trải nghiệm cảm giác mạnh ngắm toàn cảnh thành phố từ trên cao.",
					time = "1 Giờ",
					numAdults = 1,
					numChildren = 0,
					totalPrice = 1200000,
					tourId = 2,
					bookingId = 102,
					checkoutId = 202,
					images = new string[] { "tour1.jpg" }
				},
				new {
					bookingStatus = "f",
					destination = "Cù Lao Chàm",
					rating = 5,
					title = "Tour tham quan đảo Cù Lao Chàm",
					description = "Thưởng thức hải sản tươi sống và tham quan các di tích lịch sử.",
					time = "1 Ngày",
					numAdults = 4,
					numChildren = 2,
					totalPrice = 4800000,
					tourId = 3,
					bookingId = 103,
					checkoutId = 203,
					images = new string[] { "tour1.jpg" }
				}
			};

			// Truyền dữ liệu ra View thông qua ViewBag hoặc Model
			ViewBag.MyTours = myTours;

			return View();
		}
	}
}