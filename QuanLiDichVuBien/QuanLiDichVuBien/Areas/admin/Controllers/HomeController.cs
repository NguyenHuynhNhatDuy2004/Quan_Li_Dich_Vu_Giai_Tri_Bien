using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.admin.Controllers
{
	[Area("admin")]
	public class HomeController : Controller
	{
		
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}

	}
}
