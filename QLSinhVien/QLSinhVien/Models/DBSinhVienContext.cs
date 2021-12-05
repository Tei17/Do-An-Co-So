using System.Data.Entity;

namespace QLSinhVien.Models
{
	public partial class DBSinhVienContext : DbContext
	{
		public DBSinhVienContext()
			: base("name=DBSinhVienConnectionString")
		{
		}

		public virtual DbSet<ChucVu> ChucVus { get; set; }
		public virtual DbSet<SinhVien> SinhViens { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ChucVu>()
				.HasMany(e => e.SinhViens)
				.WithOptional(e => e.ChucVu)
				.HasForeignKey(e => e.IdChucVu);
		}
	}
}
