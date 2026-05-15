using System.Collections.Generic;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class RoleClaimsViewModel
	{
		public string RoleId { get; set; }
		public string RoleName { get; set; }

		// Danh sách các quyền thuộc về Role này
		public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
	}
}