// Areas/admin/Controllers/CouponController.cs
using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class CouponController : Controller
	{
		private static List<CouponViewModel> coupons = new List<CouponViewModel>()
		{
			new CouponViewModel
			{
				CouponID    = Guid.NewGuid(),
				Code        = "SUMMER20",
				Description = "Giảm 20% mùa hè",
				DiscountType     = DiscountType.Percent,
				DiscountValue    = 20,
				MaxDiscountAmount = 500000,
				MinOrderAmount   = 1000000,
				StartDate   = DateTime.Now.AddDays(-5),
				EndDate     = DateTime.Now.AddDays(25),
				Quantity    = 100,
				UsedCount   = 12,
				IsActive    = true,
				CreateAt    = DateTime.Now.AddDays(-5),
				ModifyAt    = DateTime.Now.AddDays(-5),
			},
			new CouponViewModel
			{
				CouponID    = Guid.NewGuid(),
				Code        = "GIAM100K",
				Description = "Giảm 100.000₫ cho đơn từ 500K",
				DiscountType     = DiscountType.Amount,
				DiscountValue    = 100000,
				MinOrderAmount   = 500000,
				StartDate   = DateTime.Now.AddDays(-2),
				EndDate     = DateTime.Now.AddDays(10),
				Quantity    = 50,
				UsedCount   = 50, // Hết lượt
                IsActive    = true,
				CreateAt    = DateTime.Now.AddDays(-2),
				ModifyAt    = DateTime.Now.AddDays(-2),
			},
			new CouponViewModel
			{
				CouponID    = Guid.NewGuid(),
				Code        = "VIP50",
				Description = "Giảm 50% dành cho VIP",
				DiscountType     = DiscountType.Percent,
				DiscountValue    = 50,
				MaxDiscountAmount = 2000000,
				MinOrderAmount   = 5000000,
				StartDate   = DateTime.Now.AddDays(5),  // Chưa bắt đầu
                EndDate     = DateTime.Now.AddDays(35),
				Quantity    = 20,
				UsedCount   = 0,
				IsActive    = false,
				CreateAt    = DateTime.Now,
				ModifyAt    = DateTime.Now,
			}
		};

		// ========================= INDEX =========================
		public IActionResult Index()
		{
			ViewData["ActivePage"] = "CouponManager";
			return View(coupons);
		}

		// ========================= CREATE GET =========================
		public IActionResult Create()
		{
			var model = new CouponViewModel
			{
				StartDate = DateTime.Now,
				EndDate = DateTime.Now.AddDays(30),
				Quantity = 100,
				IsActive = true
			};
			ViewData["ActivePage"] = "CouponManager";
			return View(model);
		}

		// ========================= CREATE POST =========================
		[HttpPost]
		public IActionResult Create(CouponViewModel model)
		{
			// Kiểm tra trùng mã
			if (coupons.Any(c => c.Code.ToUpper() == model.Code.ToUpper()))
				ModelState.AddModelError("Code", "Mã giảm giá này đã tồn tại");

			// Kiểm tra ngày
			if (model.EndDate <= model.StartDate)
				ModelState.AddModelError("EndDate", "Ngày kết thúc phải sau ngày bắt đầu");

			// Kiểm tra % không vượt 100
			if (model.DiscountType == DiscountType.Percent && model.DiscountValue > 100)
				ModelState.AddModelError("DiscountValue", "Phần trăm giảm không được vượt quá 100%");

			if (ModelState.IsValid)
			{
				model.CouponID = Guid.NewGuid();
				model.Code = model.Code.ToUpper().Trim();
				model.UsedCount = 0;
				model.CreateAt = DateTime.Now;
				model.ModifyAt = DateTime.Now;

				coupons.Add(model);

				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành công!";
				TempData["NotificationMessage"] = $"Tạo mã \"{model.Code}\" thành công!";
				return RedirectToAction("Index");
			}

			ViewData["ActivePage"] = "CouponManager";
			return View(model);
		}

		// ========================= UPDATE GET =========================
		public IActionResult Update(Guid CouponID)
		{
			var coupon = coupons.FirstOrDefault(c => c.CouponID == CouponID);
			if (coupon == null) return RedirectToAction("Index");

			ViewData["ActivePage"] = "CouponManager";
			return View(coupon);
		}

		// ========================= UPDATE POST =========================
		[HttpPost]
		public IActionResult Update(CouponViewModel model)
		{
			// Kiểm tra trùng mã (trừ chính nó)
			if (coupons.Any(c => c.Code.ToUpper() == model.Code.ToUpper()
							  && c.CouponID != model.CouponID))
				ModelState.AddModelError("Code", "Mã giảm giá này đã tồn tại");

			if (model.EndDate <= model.StartDate)
				ModelState.AddModelError("EndDate", "Ngày kết thúc phải sau ngày bắt đầu");

			if (model.DiscountType == DiscountType.Percent && model.DiscountValue > 100)
				ModelState.AddModelError("DiscountValue", "Phần trăm giảm không được vượt quá 100%");

			if (ModelState.IsValid)
			{
				var coupon = coupons.FirstOrDefault(c => c.CouponID == model.CouponID);
				if (coupon != null)
				{
					coupon.Code = model.Code.ToUpper().Trim();
					coupon.Description = model.Description;
					coupon.DiscountType = model.DiscountType;
					coupon.DiscountValue = model.DiscountValue;
					coupon.MaxDiscountAmount = model.MaxDiscountAmount;
					coupon.MinOrderAmount = model.MinOrderAmount;
					coupon.StartDate = model.StartDate;
					coupon.EndDate = model.EndDate;
					coupon.Quantity = model.Quantity;
					coupon.IsActive = model.IsActive;
					coupon.ModifyAt = DateTime.Now;

					TempData["NotificationType"] = "success";
					TempData["NotificationTitle"] = "Thành công!";
					TempData["NotificationMessage"] = $"Cập nhật mã \"{coupon.Code}\" thành công!";
				}
				return RedirectToAction("Index");
			}

			ViewData["ActivePage"] = "CouponManager";
			return View(model);
		}

		// ========================= TOGGLE ACTIVE =========================
		public IActionResult ToggleActive(Guid CouponID)
		{
			var coupon = coupons.FirstOrDefault(c => c.CouponID == CouponID);
			if (coupon != null)
			{
				coupon.IsActive = !coupon.IsActive;
				coupon.ModifyAt = DateTime.Now;

				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành công!";
				TempData["NotificationMessage"] = coupon.IsActive
					? $"Đã kích hoạt mã \"{coupon.Code}\""
					: $"Đã tắt mã \"{coupon.Code}\"";
			}
			return RedirectToAction("Index");
		}

		// ========================= DELETE =========================
		public IActionResult Delete(Guid CouponID)
		{
			var coupon = coupons.FirstOrDefault(c => c.CouponID == CouponID);
			if (coupon != null)
			{
				coupons.Remove(coupon);
				TempData["NotificationType"] = "success";
				TempData["NotificationTitle"] = "Thành công!";
				TempData["NotificationMessage"] = "Xóa mã giảm giá thành công!";
			}
			return RedirectToAction("Index");
		}

		// ========================= APPLY (dùng cho Booking) =========================
		[HttpPost]
		public IActionResult Apply(string code, decimal orderAmount)
		{
			var coupon = coupons.FirstOrDefault(c =>
				c.Code == code.ToUpper().Trim() && c.IsValid);

			if (coupon == null)
				return Json(new { success = false, message = "Mã không hợp lệ hoặc đã hết hạn" });

			if (orderAmount < coupon.MinOrderAmount)
				return Json(new
				{
					success = false,
					message = $"Đơn hàng tối thiểu {string.Format("{0:N0}₫", coupon.MinOrderAmount)}"
				});

			decimal discount = coupon.DiscountType == DiscountType.Percent
				? orderAmount * coupon.DiscountValue / 100
				: coupon.DiscountValue;

			// Giới hạn tối đa nếu là %
			if (coupon.DiscountType == DiscountType.Percent && coupon.MaxDiscountAmount.HasValue)
				discount = Math.Min(discount, coupon.MaxDiscountAmount.Value);

			discount = Math.Min(discount, orderAmount); // Không giảm quá tổng tiền

			return Json(new
			{
				success = true,
				discount = discount,
				finalAmount = orderAmount - discount,
				message = $"Áp dụng thành công! Giảm {string.Format("{0:N0}₫", discount)}"
			});
		}
	}
}