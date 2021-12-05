namespace QLSinhVien.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("SinhVien")]
	public partial class SinhVien
	{
		public long Id { get; set; }

		[Required]
		[StringLength(200)]
		[Display(Name = "Họ và tên")]
		public string HoVaTen { get; set; }
		[Display(Name = "Giới tính")]
		public bool GioiTinh { get; set; }

		[StringLength(200)]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[StringLength(20)]
		[Display(Name = "Số điện thoại")]
		public string SoDienThoai { get; set; }

		[StringLength(200)]
		[Display(Name = "MSSV")]
		public string MaSoSV { get; set; }

		[StringLength(200)]
		[Display(Name = "Tên đăng nhập")]
		public string TenDangNhap { get; set; }

		[StringLength(200)]
		[Display(Name = "Mật khẩu")]
		public string MatKhau { get; set; }
		[Display(Name = "Chức vụ")]
		public long? IdChucVu { get; set; }
		[Display(Name = "Là quản trị")]
		public bool LaQuanTri { get; set; }
		[Display(Name = "Là chuyên viên")]
		public bool LaChuyenVien { get; set; }
		[Display(Name = "Là sinh viên")]
		public bool LaSinhVien { get; set; }

		public virtual ChucVu ChucVu { get; set; }
	}
}
