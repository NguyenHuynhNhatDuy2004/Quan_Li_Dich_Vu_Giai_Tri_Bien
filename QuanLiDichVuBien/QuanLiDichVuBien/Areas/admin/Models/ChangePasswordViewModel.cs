using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class ChangePasswordViewModel
	{
		public Guid userId { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu mới")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
		[Display(Name = "Xác nhận mật khẩu")]
		public string ConfirmPassword { get; set; }
	}
}