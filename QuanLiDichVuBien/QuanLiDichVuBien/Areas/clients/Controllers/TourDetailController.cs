using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.clients.Models.ViewModels;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class TourDetailController : Controller
	{
		// GET: /clients/TourDetail/Index/1
		public IActionResult Index(int id = 1)
		{
			var vm = GetMockTourDetail(id);
			ViewData["Title"] = "Chi tiết tours";
			return View(vm);
		}

		// POST: /clients/TourDetail/Reviews  (AJAX)
		[HttpPost]
		public IActionResult Reviews(int tourId, string message, int rating)
		{
			// TODO: lưu review vào DB
			// Tạm thời trả về mock data
			var updatedDetail = GetMockTourDetail(tourId);

			// Thêm review mới vào list mock để thấy thay đổi trên UI
			updatedDetail.Reviews.Insert(0, new ReviewViewModel
			{
				UserName = "Bạn",
				Comment = message,
				Rating = rating,
				CreatedAt = DateTime.Now
			});

			// Render partial view trả về cho AJAX
			return PartialView("~/Areas/clients/Views/Partials/_Reviews.cshtml", updatedDetail);
		}
		// POST: /clients/TourDetail/Booking
		[HttpPost]
		public IActionResult Booking(int id)
		{
			return RedirectToAction("Index", "Booking", new { area = "clients", id = id });
		}

		// -------------------------------------------------------------------
		// MOCK DATA – xóa / thay bằng service/repo khi có database
		// -------------------------------------------------------------------
		private TourDetailViewModel GetMockTourDetail(int id)
		{
			return new TourDetailViewModel
			{
				TourId = id,

				Title = "Trải nghiệm Jetski tốc độ cao",

				Destination = "Biển Mỹ Khê - Đà Nẵng",

				Domain = "t",

				Time = "30 phút",

				PriceAdult = 800000,

				PriceChild = 500000,

				Rating = 4.9,

				AverageStar = 5,

				ReviewCount = 25,

				StartDate = DateTime.Now.AddDays(1),

				EndDate = DateTime.Now.AddDays(1),

				Quantity = 20,

				CanReview = true,

				Images = new List<string>
		{
			"jetski1.jpg",
			"jetski2.png",
			"jetski3.webp",
			"jetski4.jpg",
			"jetski5.jpg"
		},

				Description =
					"<p>Dịch vụ Jetski tại biển Mỹ Khê mang đến trải nghiệm tốc độ cực kỳ hấp dẫn dành cho du khách yêu thích cảm giác mạnh.</p>" +

					"<p>Người chơi sẽ được hướng dẫn kỹ năng điều khiển Jetski, trang bị đầy đủ áo phao và thiết bị an toàn trước khi tham gia.</p>" +

					"<p>Đây là một trong những hoạt động giải trí biển được yêu thích nhất tại Đà Nẵng.</p>",

				Timeline = new List<TimelineViewModel>
		{
			new TimelineViewModel
			{
				TimeLineId = 1,
				Title = "Tiếp nhận khách hàng",
				Description =
					"<p>Khách hàng check-in tại khu vực dịch vụ và nhận hướng dẫn từ nhân viên.</p>"
			},

			new TimelineViewModel
			{
				TimeLineId = 2,
				Title = "Trang bị bảo hộ",
				Description =
					"<p>Cung cấp áo phao, kính bảo hộ và hướng dẫn quy tắc an toàn khi tham gia.</p>"
			},

			new TimelineViewModel
			{
				TimeLineId = 3,
				Title = "Trải nghiệm Jetski",
				Description =
					"<p>Điều khiển Jetski trên biển cùng huấn luyện viên hỗ trợ trong suốt quá trình.</p>"
			},

			new TimelineViewModel
			{
				TimeLineId = 4,
				Title = "Kết thúc dịch vụ",
				Description =
					"<p>Khách hàng nghỉ ngơi, chụp ảnh lưu niệm và đánh giá trải nghiệm.</p>"
			}
		},

				Reviews = new List<ReviewViewModel>
		{
			new ReviewViewModel
			{
				UserName = "Nguyễn Văn A",
				Comment = "Dịch vụ rất chuyên nghiệp, cảm giác cực kỳ đã!",
				Rating = 5,
				CreatedAt = DateTime.Now.AddDays(-2)
			},

			new ReviewViewModel
			{
				UserName = "Trần Minh K",
				Comment = "Nhân viên hỗ trợ nhiệt tình và an toàn.",
				Rating = 5,
				CreatedAt = DateTime.Now.AddDays(-5)
			}
		},

				RelatedTours = new List<TourViewModel>
		{
			new TourViewModel
			{
				TourId = 2,
				Title = "Flyboard nghệ thuật",

				Destination = "Biển Phạm Văn Đồng",

				Rating = 4.8,

				Images = new List<string>
				{
					"flyboard1.jpg"
				}
			},

			new TourViewModel
			{
				TourId = 3,
				Title = "Chèo SUP bình minh",

				Destination = "Biển Mỹ Khê",

				Rating = 4.7,

				Images = new List<string>
				{
					"sup1.jpg"
				}
			}
		}
			};
		}
	
	}
}