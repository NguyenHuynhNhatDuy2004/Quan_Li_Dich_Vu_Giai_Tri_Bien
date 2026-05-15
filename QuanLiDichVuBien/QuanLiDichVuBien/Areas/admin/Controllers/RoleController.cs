using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLiDichVuBien.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class RoleController : Controller
	{
		// DATA GIẢ - Để static để không bị mất khi chuyển trang
		private static List<RoleClaimsViewModel> rolesData = new List<RoleClaimsViewModel>()
		{
			new RoleClaimsViewModel
			{
				RoleId = Guid.NewGuid().ToString(),
				RoleName = "Admin",
				Claims = new List<ClaimViewModel>
				{
					new ClaimViewModel { Type = "permission", Value = "user-view", Description = "Xem danh sách người dùng", Selected = true },
					new ClaimViewModel { Type = "permission", Value = "role-manage", Description = "Quản lý quyền hạn", Selected = true }
				}
			},
			new RoleClaimsViewModel
			{
				RoleId = Guid.NewGuid().ToString(),
				RoleName = "Manager",
				Claims = new List<ClaimViewModel>()
			}
		};

		public IActionResult Index()
		{
			ViewData["ActivePage"] = "AccountManager";
			return View(rolesData);
		}

		[HttpGet]
		public IActionResult ChangeRole(string userId)
		{
			var model = new RoleChangeViewModel
			{
				UserId = userId ?? Guid.NewGuid().ToString(),
				UserName = "Người dùng demo",
				CurrentRole = "Admin",
				Roles = rolesData.Select(r => new SelectListItem { Value = r.RoleName, Text = r.RoleName }).ToList()
			};
			ViewData["ActivePage"] = "AccountManager";
			return View(model);
		}

		[HttpPost]
		public IActionResult UpdateRole(RoleChangeViewModel model)
		{
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateRoleClaims(string roleId)
		{
			var role = rolesData.FirstOrDefault(r => r.RoleId == roleId);
			if (role == null) return RedirectToAction("Index");
			ViewData["ActivePage"] = "AccountManager";
			return View(role);
		}

		[HttpPost]
		public IActionResult UpdateRoleClaims(string roleId, RoleClaimsViewModel model)
		{
			return RedirectToAction("UpdateRoleClaims", new { roleId = roleId });
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewData["ActivePage"] = "AccountManager";
			return View(new RoleCreateViewModel());
		}

		[HttpPost]
		public IActionResult Create(RoleCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				rolesData.Add(new RoleClaimsViewModel
				{
					RoleId = Guid.NewGuid().ToString(),
					RoleName = model.Name,
					Claims = new List<ClaimViewModel>()
				});
				return RedirectToAction("Index");
			}
			return View(model);
		}

		// SỬA LỖI DÒNG 202: Chuyển tham số về object để linh hoạt, tránh lỗi kiểu dữ liệu
		public IActionResult Delete(object roleId)
		{
			string idString = roleId?.ToString();
			var role = rolesData.FirstOrDefault(r => r.RoleId == idString);
			if (role != null) rolesData.Remove(role);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult DeleteClaim(string roleId, string value)
		{
			var role = rolesData.FirstOrDefault(r => r.RoleId == roleId);
			var claim = role?.Claims.FirstOrDefault(c => c.Value == value);
			if (claim != null) role.Claims.Remove(claim);
			return RedirectToAction("UpdateRoleClaims", new { roleId = roleId });
		}
	}
}