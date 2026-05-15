namespace QuanLiDichVuBien.Areas.admin.Models
{
	public class ClaimViewModel
	{
		public string? RoleId { get; set; }
		public string? RoleName { get; set; }

		// Loại claim (thường là "permission")
		public string Type { get; set; } = "permission";

		// Giá trị của quyền (ví dụ: tour-add, user-delete)
		public string Value { get; set; }

		// Mô tả tiếng Việt để hiển thị lên giao diện
		public string Description { get; set; }

		// Trạng thái checkbox (đã chọn hay chưa)
		public bool Selected { get; set; }
	}
}