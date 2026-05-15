using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLiDichVuBien.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class BookingController : Controller
	{
		// DATA GIẢ - Danh sách đặt tour
		private static List<BookingViewModel> bookings = new List<BookingViewModel>()
		{
			new BookingViewModel
{
	BookingID = Guid.NewGuid(),
	CustomerID = Guid.NewGuid(),
	TourID = Guid.NewGuid(),
	Adult = 2,
	Child = 1,
	TotalPrice = 3500000,
	CreateAt = DateTime.Now.AddDays(-5),
	ModifyAt = DateTime.Now.AddDays(-5),
	CustomerName = "Nguyễn Văn Duy",
	CustomerPhone = "0901234567",   // Thêm
    CustomerEmail = "duy@gmail.com", // Thêm
    TourTitle = "Tour Lặn Ngắm San Hô Sơn Trà",
	PaymentStatus = true
},
new BookingViewModel
{
	BookingID = Guid.NewGuid(),
	CustomerID = Guid.NewGuid(),
	TourID = Guid.NewGuid(),
	Adult = 4,
	Child = 0,
	TotalPrice = 8000000,
	CreateAt = DateTime.Now.AddDays(-2),
	ModifyAt = DateTime.Now.AddDays(-1),
	CustomerName = "Lê Thanh Sơn",
	CustomerPhone = "0987654321",    // Thêm
    CustomerEmail = "son@gmail.com", // Thêm
    TourTitle = "Tour Nghỉ Dưỡng Mỹ Khê",
	PaymentStatus = false
}
		};

		// =========================
		// INDEX
		// =========================
		public IActionResult Index()
		{
			foreach (var item in bookings)
			{
				// Giả lập ViewData Status giống code gốc của bạn
				ViewData[$"Status_{item.BookingID}"] = item.PaymentStatus;
			}

			ViewData["ActivePage"] = "TourManager";
			return View(bookings);
		}

		// =========================
		// CREATE GET
		// =========================
		public IActionResult Create()
		{
			// Giả lập danh sách khách hàng để chọn
			var listCustomer = new List<SelectListItem>
			{
				new SelectListItem { Value = Guid.NewGuid().ToString(), Text = "Nguyễn Văn Duy" },
				new SelectListItem { Value = Guid.NewGuid().ToString(), Text = "Lê Thanh Sơn" }
			};
			ViewBag.ListCustomer = new SelectList(listCustomer, "Value", "Text");

			// Giả lập danh sách tour để chọn
			var listTour = new List<SelectListItem>
			{
				new SelectListItem { Value = Guid.NewGuid().ToString(), Text = "Tour Bán Đảo Sơn Trà" },
				new SelectListItem { Value = Guid.NewGuid().ToString(), Text = "Tour Biển Mỹ Khê" }
			};
			ViewBag.ListTour = new SelectList(listTour, "Value", "Text");

			ViewData["ActivePage"] = "TourManager";
			return View();
		}

		// =========================
		// CREATE POST
		// =========================
		[HttpPost]
		public IActionResult Create(BookingViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.BookingID = Guid.NewGuid();
				model.CreateAt = DateTime.Now;
				model.ModifyAt = DateTime.Now;
				model.TourTitle = "Tour Mới Đặt"; // Giả định tên
				model.CustomerName = "Khách Hàng Mới";

				bookings.Add(model);

				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành công!";
				TempData["NotificationMessage"] = "Đặt Tour thành công!";
				return RedirectToAction("Index");
			}

			return View(model);
		}

		// =========================
		// UPDATE GET
		// =========================
		public IActionResult Update(Guid BookingID)
		{
			var booking = bookings.FirstOrDefault(x => x.BookingID == BookingID);
			if (booking == null) return RedirectToAction("Index");

			ViewData["ActivePage"] = "TourManager";
			return View(booking);
		}

		// =========================
		// UPDATE POST
		// =========================
		[HttpPost]
		public IActionResult Update(BookingViewModel model)
		{
			var booking = bookings.FirstOrDefault(x => x.BookingID == model.BookingID);
			if (booking != null)
			{
				booking.Adult = model.Adult;
				booking.Child = model.Child;
				booking.TotalPrice = model.TotalPrice;
				booking.ModifyAt = DateTime.Now;

				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành Công!";
				TempData["NotificationMessage"] = "Cập nhật đặt tour thành công!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

		// =========================
		// DELETE (Cancel)
		// =========================
		public IActionResult Delete(Guid BookingID)
		{
			var booking = bookings.FirstOrDefault(x => x.BookingID == BookingID);
			if (booking != null)
			{
				bookings.Remove(booking);
				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành công!";
				TempData["NotificationMessage"] = "Xóa thông tin đặt tour thành công";
			}
			return RedirectToAction("Index");
		}
	}
}