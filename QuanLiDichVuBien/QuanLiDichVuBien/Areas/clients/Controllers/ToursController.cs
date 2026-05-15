using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.clients.Models.ViewModels;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class ToursController : Controller
	{
		// GET: /clients/Tours
		public IActionResult Index()
		{
			var tours = GetMockTours();

			var vm = new ToursPageViewModel
			{
				DomainsCount = new DomainCountViewModel
				{
					MienBac = 0,
					MienTrung = tours.Count(x => x.Domain == "t"),
					MienNam = tours.Count(x => x.Domain == "n")
				},

				PopularTours = GetMockPopularTours(),

				Tours = tours
			};

			ViewData["Title"] = "Dịch vụ giải trí biển";

			return View(vm);
		}

		// =========================================================
		// MOCK DATA
		// =========================================================
		private List<TourViewModel> GetMockTours()
		{
			return new List<TourViewModel>
			{
                // JETSKI
                new TourViewModel
				{
					TourId = 1,
					Title = "Trải nghiệm Jetski tốc độ cao",
					Destination = "Biển Mỹ Khê - Đà Nẵng",
					Domain = "t",
					Time = "30 phút",
					PriceAdult = 800000,
					PriceChild = 500000,
					Rating = 4.9,
					StartDate = DateTime.Now.AddDays(1),
					EndDate = DateTime.Now.AddDays(1),
					Quantity = 20,

					Images = new List<string>
					{
						"jetski1.jpg",
						"jetski2.jpg"
					}
				},

                // FLYBOARD
                new TourViewModel
				{
					TourId = 2,
					Title = "Flyboard bay trên mặt biển",
					Destination = "Biển Phạm Văn Đồng",
					Domain = "t",
					Time = "45 phút",
					PriceAdult = 1200000,
					PriceChild = 0,
					Rating = 4.8,
					StartDate = DateTime.Now.AddDays(2),
					EndDate = DateTime.Now.AddDays(2),
					Quantity = 10,

					Images = new List<string>
					{
						"flyboard1.jpg",
						"flyboard2.jpg"
					}
				},

                // SUP
                new TourViewModel
				{
					TourId = 3,
					Title = "Chèo SUP ngắm bình minh",
					Destination = "Biển Mỹ Khê",
					Domain = "t",
					Time = "60 phút",
					PriceAdult = 350000,
					PriceChild = 200000,
					Rating = 4.7,
					StartDate = DateTime.Now.AddDays(3),
					EndDate = DateTime.Now.AddDays(3),
					Quantity = 25,

					Images = new List<string>
					{
						"sup1.jpg",
						"sup2.jpg"
					}
				},

                // CANO
                new TourViewModel
				{
					TourId = 4,
					Title = "Tour Cano khám phá Sơn Trà",
					Destination = "Bán đảo Sơn Trà",
					Domain = "t",
					Time = "Nửa ngày",
					PriceAdult = 950000,
					PriceChild = 650000,
					Rating = 4.9,
					StartDate = DateTime.Now.AddDays(5),
					EndDate = DateTime.Now.AddDays(5),
					Quantity = 18,

					Images = new List<string>
					{
						"cano1.jpg",
						"cano2.jpg"
					}
				},

                // LẶN BIỂN
                new TourViewModel
				{
					TourId = 5,
					Title = "Lặn biển ngắm san hô",
					Destination = "Sơn Trà - Đà Nẵng",
					Domain = "t",
					Time = "2 giờ",
					PriceAdult = 700000,
					PriceChild = 450000,
					Rating = 4.6,
					StartDate = DateTime.Now.AddDays(4),
					EndDate = DateTime.Now.AddDays(4),
					Quantity = 15,

					Images = new List<string>
					{
						"diving1.jpg",
						"diving2.jpg"
					}
				},

                // DU THUYỀN
                new TourViewModel
				{
					TourId = 6,
					Title = "Du thuyền ngắm hoàng hôn",
					Destination = "Sông Hàn - Đà Nẵng",
					Domain = "t",
					Time = "90 phút",
					PriceAdult = 600000,
					PriceChild = 350000,
					Rating = 4.5,
					StartDate = DateTime.Now.AddDays(6),
					EndDate = DateTime.Now.AddDays(6),
					Quantity = 30,

					Images = new List<string>
					{
						"yacht1.jpg",
						"yacht2.jpg"
					}
				}
			};
		}

		// =========================================================
		// POPULAR SERVICES
		// =========================================================
		private List<TourViewModel> GetMockPopularTours()
		{
			return new List<TourViewModel>
			{
				new TourViewModel
				{
					TourId = 1,
					Title = "Jetski tốc độ cao",
					Destination = "Biển Mỹ Khê",
					Rating = 4.9,

					Images = new List<string>
					{
						"jetski1.jpg"
					}
				},

				new TourViewModel
				{
					TourId = 2,
					Title = "Flyboard chuyên nghiệp",
					Destination = "Đà Nẵng",
					Rating = 4.8,

					Images = new List<string>
					{
						"flyboard1.jpg"
					}
				},

				new TourViewModel
				{
					TourId = 3,
					Title = "Tour Cano Sơn Trà",
					Destination = "Sơn Trà",
					Rating = 4.9,

					Images = new List<string>
					{
						"cano1.jpg"
					}
				}
			};
		}
	}
}