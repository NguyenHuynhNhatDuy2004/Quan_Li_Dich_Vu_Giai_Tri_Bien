using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	public class BookingController : Controller
	{
		[Area("clients")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
