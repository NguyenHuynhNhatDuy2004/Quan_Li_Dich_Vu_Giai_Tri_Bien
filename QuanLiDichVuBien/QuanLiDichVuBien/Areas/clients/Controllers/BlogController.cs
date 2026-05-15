using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")] // Đặt ở đây để áp dụng cho cả Index và Details
	[Route("clients/[controller]/[action]")] // Thêm cái này nếu bạn muốn route rõ ràng
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details()
		{
			return View(); 
		}
	}
}