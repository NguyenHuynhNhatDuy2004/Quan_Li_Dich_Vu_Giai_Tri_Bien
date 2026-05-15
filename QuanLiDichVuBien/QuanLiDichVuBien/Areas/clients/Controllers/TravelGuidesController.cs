using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class TravelGuidesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
