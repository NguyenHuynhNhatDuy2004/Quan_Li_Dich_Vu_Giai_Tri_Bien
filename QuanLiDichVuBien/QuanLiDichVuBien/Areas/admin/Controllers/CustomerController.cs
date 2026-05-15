using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.admin.Models;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class CustomerController : Controller
	{
		// DATA GIẢ
		private static List<CustomerViewModel> customers =
			new List<CustomerViewModel>()
			{
				new CustomerViewModel
				{
					CustomerID = Guid.NewGuid(),
					FirstName = "Nguyễn",
					LastName = "Văn A",
					Address = "Đà Nẵng",
					Phone = "0905000001",
					Email = "vana",
					Image = "avatar1.jpg"
				},

				new CustomerViewModel
				{
					CustomerID = Guid.NewGuid(),
					FirstName = "Trần",
					LastName = "Thị B",
					Address = "Huế",
					Phone = "0905000002",
					Email = "thib",
					Image = "avatar2.jpg"
				},

				new CustomerViewModel
				{
					CustomerID = Guid.NewGuid(),
					FirstName = "Lê",
					LastName = "Văn C",
					Address = "Hà Nội",
					Phone = "0905000003",
					Email = "vanc",
					Image = "avatar3.jpg"
				}
			};

		// =========================
		// INDEX
		// =========================
		public IActionResult Index()
		{
			return View(customers);
		}

		// =========================
		// CREATE GET
		// =========================
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		// =========================
		// CREATE POST
		// =========================
		[HttpPost]
		public IActionResult Create(CustomerViewModel customer)
		{
			customer.CustomerID = Guid.NewGuid();

			// Ảnh giả
			if (string.IsNullOrEmpty(customer.Image))
			{
				customer.Image = "avatar1.jpg";
			}

			customers.Add(customer);

			return RedirectToAction("Index");
		}

		// =========================
		// UPDATE GET
		// =========================
		[HttpGet]
		public IActionResult Update(Guid CustomerID)
		{
			var customer = customers.FirstOrDefault(x => x.CustomerID == CustomerID);

			if (customer == null)
			{
				return RedirectToAction("Index");
			}

			return View(customer);
		}

		// =========================
		// UPDATE POST
		// =========================
		[HttpPost]
		public IActionResult Update(CustomerViewModel model)
		{
			var customer = customers.FirstOrDefault(x => x.CustomerID == model.CustomerID);

			if (customer != null)
			{
				customer.FirstName = model.FirstName;
				customer.LastName = model.LastName;
				customer.Address = model.Address;
				customer.Phone = model.Phone;
				customer.Email = model.Email;
			}

			return RedirectToAction("Index");
		}

		// =========================
		// DELETE
		// =========================
		public IActionResult Delete(Guid CustomerID)
		{
			var customer = customers.FirstOrDefault(x => x.CustomerID == CustomerID);

			if (customer != null)
			{
				customers.Remove(customer);
			}

			return RedirectToAction("Index");
		}
	}
}