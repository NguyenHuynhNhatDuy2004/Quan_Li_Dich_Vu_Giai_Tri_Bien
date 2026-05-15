using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			
			var username = HttpContext.Session.GetString("Username");
			if (string.IsNullOrEmpty(username))
				return RedirectToAction("Index", "Login");

			ViewBag.Username = username;
			return View();
		}
	}
}