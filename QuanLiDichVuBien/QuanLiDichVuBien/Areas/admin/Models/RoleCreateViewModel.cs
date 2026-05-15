using System.ComponentModel.DataAnnotations;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class RoleCreateViewModel
	{
		[Required(ErrorMessage = "Tên vai trò không được để trống")]
		[Display(Name = "Tên vai trò")]
		public string Name { get; set; }
	}
}