using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class AccountController : Controller
	{
		// DATA GIẢ - Duy trì danh sách tài khoản trong bộ nhớ
		private static List<AccountViewModel> accounts = new List<AccountViewModel>()
		{
			new AccountViewModel
			{
				Id = Guid.NewGuid().ToString(),
				Username = "admin_duy",
				Password = "hashed_password_1",
				Email = "duynguyen@gmail.com",
				Phone = "0905123456",
				Roles = new List<string> { "Admin" },
				isActive = true
			},
			new AccountViewModel
			{
				Id = Guid.NewGuid().ToString(),
				Username = "manager_son",
				Password = "hashed_password_2",
				Email = "sonle@gmail.com",
				Phone = "0905999888",
				Roles = new List<string> { "Manager" },
				isActive = true
			},
			new AccountViewModel
			{
				Id = Guid.NewGuid().ToString(),
				Username = "staff_anh",
				Password = "hashed_password_3",
				Email = "minhanh@gmail.com",
				Phone = "0905111222",
				Roles = new List<string> { "Staff" },
				isActive = false // Tài khoản đang bị khóa
            }
		};

		// =========================
		// INDEX - Danh sách tài khoản
		// =========================
		public IActionResult Index()
		{
			ViewData["ActivePage"] = "AccountManager";
			return View(accounts);
		}

		// =========================
		// BLOCK / UNBLOCK USER
		// =========================
		public IActionResult BlockUser(string userId)
		{
			var user = accounts.FirstOrDefault(u => u.Id == userId);
			if (user == null)
			{
				TempData["NotificationType"] = "danger";
				TempData["NotificationTitle"] = "Thất bại!";
				TempData["NotificationMessage"] = "Không tìm thấy tài khoản!";
				return RedirectToAction("Index");
			}

			// Đảo ngược trạng thái Active
			user.isActive = !user.isActive;

			TempData["NotificationType"] = "success";
			TempData["NotificationTitle"] = "Thành công!";
			TempData["NotificationMessage"] = user.isActive
				? $"Đã mở khóa tài khoản {user.Email}"
				: $"Đã khóa tài khoản {user.Email}";

			return RedirectToAction("Index");
		}

		// =========================
		// CHANGE PASSWORD - GET
		// =========================
		[HttpGet]
		public IActionResult ChangePassword(string userId)
		{
			if (string.IsNullOrEmpty(userId)) return RedirectToAction("Index");

			// PHẢI truyền một object Model mới vào View để asp-for không bị lỗi dynamic
			var model = new ChangePasswordViewModel
			{
				userId = Guid.Parse(userId)
			};

			ViewData["ActivePage"] = "AccountManager";
			return View(model);
		}

		// =========================
		// CHANGE PASSWORD - POST
		// =========================
		[HttpPost]
		public IActionResult ChangePassword(string userId, ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = accounts.FirstOrDefault(u => u.Id == userId);
				if (user != null)
				{
					user.Password = model.Password; // Cập nhật mật khẩu giả định

					TempData["NotificationType"] = "success";
					TempData["NotificationTitle"] = "Thành công!";
					TempData["NotificationMessage"] = "Thay đổi mật khẩu thành công!";

					return RedirectToAction("Index");
				}

				TempData["NotificationType"] = "danger";
				TempData["NotificationTitle"] = "Thất bại!";
				TempData["NotificationMessage"] = "Không tìm thấy tài khoản!";
				return RedirectToAction("Index");
			}

			ViewData["ActivePage"] = "AccountManager";
			return View(model);
		}
	}
}