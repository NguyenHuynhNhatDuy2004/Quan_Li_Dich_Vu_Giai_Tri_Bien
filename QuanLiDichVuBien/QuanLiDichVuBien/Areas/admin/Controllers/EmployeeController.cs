using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.admin.Models;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class EmployeeController : Controller
	{
		// DỮ LIỆU GIẢ (MOCK DATA)
		private static List<EmployeeViewModel> employees = new List<EmployeeViewModel>()
		{
			new EmployeeViewModel
			{
				EmployeeID = Guid.NewGuid(),
				FirstName = "Minh",
				LastName = "Điền",
				Position = "Founder",
				Address = "Gia Lai",
				Phone = "0123456789",
				Email = "dien.dev@gmail.com",
				Image = "guide-dien.jpg"
			},
			new EmployeeViewModel
			{
				EmployeeID = Guid.NewGuid(),
				FirstName = "Bảo",
				LastName = "Ngân",
				Position = "Co-founder",
				Address = "Đà Nẵng",
				Phone = "0987654321",
				Email = "baongan@gmail.com",
				Image = "guide-ngan.jpg"
			}
		};

		// =========================
		// INDEX - Danh sách nhân viên
		// =========================
		public IActionResult Index()
		{
			ViewData["ActivePage"] = "UserManager";
			return View(employees);
		}

		// =========================
		// CREATE GET - Giao diện thêm
		// =========================
		[HttpGet]
		public IActionResult Create()
		{
			ViewData["ActivePage"] = "UserManager";
			return View();
		}

		// =========================
		// CREATE POST - Xử lý thêm
		// =========================
		[HttpPost]
		public IActionResult Create(EmployeeViewModel employee)
		{
			employee.EmployeeID = Guid.NewGuid();

			// Gán ảnh mặc định nếu không chọn
			if (string.IsNullOrEmpty(employee.Image))
			{
				employee.Image = "default-user.png";
			}

			employees.Add(employee);

			TempData["NotificationType"] = "success";
			TempData["NotificationMessage"] = "Thêm nhân viên thành công!";

			return RedirectToAction("Index");
		}

		// =========================
		// UPDATE GET - Giao diện sửa
		// =========================
		[HttpGet]
		public IActionResult Update(Guid EmployeeID)
		{
			var employee = employees.FirstOrDefault(x => x.EmployeeID == EmployeeID);

			if (employee == null)
			{
				return RedirectToAction("Index");
			}

			ViewData["ActivePage"] = "UserManager";
			return View(employee);
		}

		// =========================
		// UPDATE POST - Xử lý sửa
		// =========================
		[HttpPost]
		public IActionResult Update(EmployeeViewModel model)
		{
			var employee = employees.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);

			if (employee != null)
			{
				employee.FirstName = model.FirstName;
				employee.LastName = model.LastName;
				employee.Position = model.Position;
				employee.Address = model.Address;
				employee.Phone = model.Phone;
				employee.Email = model.Email;
				// Nếu có cập nhật ảnh thì xử lý ở đây
			}

			TempData["NotificationType"] = "success";
			TempData["NotificationMessage"] = "Cập nhật thành công!";

			return RedirectToAction("Index");
		}

		// =========================
		// DELETE - Xử lý xóa
		// =========================
		public IActionResult Delete(Guid EmployeeID)
		{
			var employee = employees.FirstOrDefault(x => x.EmployeeID == EmployeeID);

			if (employee != null)
			{
				employees.Remove(employee);
				TempData["NotificationType"] = "success";
				TempData["NotificationMessage"] = "Đã xóa nhân viên!";
			}

			return RedirectToAction("Index");
		}
	}
}