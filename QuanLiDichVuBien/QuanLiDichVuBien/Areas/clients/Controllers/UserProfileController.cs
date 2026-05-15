using Microsoft.AspNetCore.Mvc;
using QuanLiDichVuBien.Areas.clients.Models;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class UserProfileController : Controller
	{
		public IActionResult Index()
		{
			// Khởi tạo dữ liệu giả
			var mockUser = new UserVM
			{
				FullName = "Duy Nguyễn",
				Email = "duynguyen.dev@gmail.com",
				PhoneNumber = "0905123456",
				Address = "470 Trần Đại Nghĩa, Ngũ Hành Sơn, Đà Nẵng",
				Avatar = "user_avatar.jpg" // Đảm bảo file này có trong wwwroot/admin/assets/images/user-profile/
			};

			return View(mockUser);
		}
	}
}