using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	public class ContactController : Controller
	{
		[Area("clients")]
		public IActionResult Index()
		{

			return View();
		}
	}
}
