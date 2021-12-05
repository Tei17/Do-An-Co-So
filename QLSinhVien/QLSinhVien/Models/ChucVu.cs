namespace QLSinhVien.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("ChucVu")]
	public partial class ChucVu
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChucVu()
		{
			SinhViens = new HashSet<SinhVien>();
		}

		public long Id { get; set; }

		[Required]
		[StringLength(200)]
		public string TenChucVu { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<SinhVien> SinhViens { get; set; }
	}
}
