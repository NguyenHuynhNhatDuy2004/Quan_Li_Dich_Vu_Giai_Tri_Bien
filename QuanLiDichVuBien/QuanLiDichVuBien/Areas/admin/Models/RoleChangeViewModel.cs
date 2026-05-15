using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class RoleChangeViewModel
	{
		public string UserId { get; set; }
		public string UserName { get; set; }

		// Vai trò hiện tại của User
		public string CurrentRole { get; set; }

		// Danh sách các vai trò có sẵn để chọn trong DropdownList
		public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
	}
}