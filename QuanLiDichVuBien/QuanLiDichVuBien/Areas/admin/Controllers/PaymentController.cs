using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class PaymentController : Controller
	{
		// DATA GIẢ - Duy trì trong bộ nhớ để test giao diện
		private static List<PaymentViewModel> payments = new List<PaymentViewModel>()
		{
			new PaymentViewModel
			{
				PaymentID = Guid.NewGuid(),
				BookingID = Guid.Parse("7a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
				Method = "Chuyển khoản Ngân hàng",
				CreateAt = DateTime.Now.AddDays(-2),
				ModifyAt = DateTime.Now.AddDays(-2),
				Total = 1500000,
				Status = true
			},
			new PaymentViewModel
			{
				PaymentID = Guid.NewGuid(),
				BookingID = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
				Method = "Thanh toán tiền mặt",
				CreateAt = DateTime.Now.AddDays(-1),
				ModifyAt = DateTime.Now,
				Total = 500000,
				Status = false
			},
			new PaymentViewModel
			{
				PaymentID = Guid.NewGuid(),
				BookingID = Guid.Parse("9f8e7d6c-5b4a-3f2e-1d0c-9b8a7f6e5d4c"),
				Method = "VNPAY",
				CreateAt = DateTime.Now.AddHours(-5),
				ModifyAt = DateTime.Now.AddHours(-5),
				Total = 2250000,
				Status = true
			}
		};

		// =========================
		// INDEX
		// =========================
		public IActionResult Index()
		{
			ViewData["ActivePage"] = "PaymentManager";
			return View(payments);
		}

		// =========================
		// CREATE GET
		// =========================
		[HttpGet]
		public IActionResult Create()
		{
			ViewData["ActivePage"] = "PaymentManager";
			return View();
		}

		// =========================
		// CREATE POST
		// =========================
		[HttpPost]
		public IActionResult Create(PaymentViewModel payment)
		{
			if (ModelState.IsValid)
			{
				payment.PaymentID = Guid.NewGuid();
				payment.CreateAt = DateTime.Now;
				payment.ModifyAt = DateTime.Now;

				payments.Add(payment);
				return RedirectToAction(nameof(Index));
			}
			return View(payment);
		}

		// =========================
		// UPDATE GET
		// =========================
		[HttpGet]
		public IActionResult Update(Guid PaymentID)
		{
			var payment = payments.FirstOrDefault(x => x.PaymentID == PaymentID);

			if (payment == null)
			{
				return RedirectToAction(nameof(Index));
			}

			ViewData["ActivePage"] = "PaymentManager";
			return View(payment);
		}

		// =========================
		// UPDATE POST
		// =========================
		[HttpPost]
		public IActionResult Update(PaymentViewModel model)
		{
			var payment = payments.FirstOrDefault(x => x.PaymentID == model.PaymentID);

			if (payment != null && ModelState.IsValid)
			{
				payment.Method = model.Method;
				payment.Status = model.Status;
				payment.Total = model.Total;
				payment.ModifyAt = DateTime.Now;

				return RedirectToAction(nameof(Index));
			}

			return View(model);
		}

		// =========================
		// DELETE
		// =========================
		[HttpPost]
		public IActionResult Delete(Guid PaymentID)
		{
			var payment = payments.FirstOrDefault(x => x.PaymentID == PaymentID);

			if (payment != null)
			{
				payments.Remove(payment);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}