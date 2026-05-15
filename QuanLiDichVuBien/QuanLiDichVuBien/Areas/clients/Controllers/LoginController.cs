using Microsoft.AspNetCore.Mvc;

namespace QuanLiDichVuBien.Areas.clients.Controllers
{
	[Area("clients")]
	public class LoginController : Controller
	{
		private static readonly List<(string Username, string Password, string Email)> FakeUsers = new()
		{
			("admin", "123456", "admin@gmail.com"),
			("user1", "123456", "user1@gmail.com"),
			("test",  "password", "test@gmail.com"),
		};

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
				return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin." });

			var user = FakeUsers.FirstOrDefault(u =>
				u.Username == username && u.Password == password);

			if (user == default)
				return Json(new { success = false, message = "Tên đăng nhập hoặc mật khẩu không đúng." });

			HttpContext.Session.SetString("Username", user.Username);
			HttpContext.Session.SetString("Email", user.Email);

			return Json(new { success = true, message = "Đăng nhập thành công!" });
		}

		[HttpPost]
		public IActionResult Register(string username_regis, string email, string password_regis)
		{
			if (string.IsNullOrWhiteSpace(username_regis) ||
				string.IsNullOrWhiteSpace(email) ||
				string.IsNullOrWhiteSpace(password_regis))
				return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin." });

			if (FakeUsers.Any(u => u.Username == username_regis))
				return Json(new { success = false, message = "Tên tài khoản đã tồn tại." });

			if (FakeUsers.Any(u => u.Email == email))
				return Json(new { success = false, message = "Email đã được sử dụng." });

			FakeUsers.Add((username_regis, password_regis, email));

			return Json(new { success = true, message = "Đăng ký thành công! Vui lòng đăng nhập." });
		}

		[HttpGet]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}