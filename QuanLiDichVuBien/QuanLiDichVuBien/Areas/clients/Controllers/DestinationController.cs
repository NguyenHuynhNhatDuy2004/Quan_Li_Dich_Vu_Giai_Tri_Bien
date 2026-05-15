using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	public class DestinationController : Controller
	{
		[Area("clients")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
