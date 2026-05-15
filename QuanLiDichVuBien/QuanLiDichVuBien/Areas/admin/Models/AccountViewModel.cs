using System.Collections.Generic;
namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class AccountViewModel
	{
		public string Id { get; set; } = "";
		public string Username { get; set; } = "";
		public string Password { get; set; } = "";
		public string Email { get; set; } = "";
		public string Phone { get; set; } = "";
		public bool isActive { get; set; }
		public IList<string> Roles { get; set; } = new List<string>();
	}
}