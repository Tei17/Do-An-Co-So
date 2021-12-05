using OfficeOpenXml;
using QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSinhVien.Controllers
{
	public class SinhVienController : Controller
	{
		// GET: SinhVien
		public ActionResult Index(string strSearch)
		{
			var listSinhVien = new DBSinhVienContext().SinhViens.ToList();
			if (!String.IsNullOrEmpty(strSearch))
			{
				listSinhVien = listSinhVien.Where(x => x.HoVaTen.Contains(strSearch)).ToList();
			}
			ViewBag.strSearch = strSearch;
			return View(listSinhVien);
		}




		// POST: SinhVien/Permission/5
		[HttpPost]
		public ActionResult Permission(SinhVien model)
		{
			try
			{
				// TODO: Add update logic here
				var context = new DBSinhVienContext();
				var oldItem = context.SinhViens.Find(model.Id);
				oldItem.LaQuanTri = model.LaQuanTri;
				oldItem.LaChuyenVien = model.LaChuyenVien;
				oldItem.LaSinhVien = model.LaSinhVien;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		[HttpPost]
		public ActionResult UploadFile(HttpPostedFileBase file)
		{
			try
			{
				if (file.ContentLength > 0)
				{
					string fileName = Path.GetFileName(file.FileName);
					if (!CheckExcelFile(fileName))
					{
						throw new Exception();
					}
					string _path = Path.Combine(Server.MapPath("~/UploadFile"), fileName);
					file.SaveAs(_path);
					var datatable = ImportExcelAsDataTable(_path);
					var ds = ConvertDataTableToList(datatable);
					var context = new DBSinhVienContext();
					foreach(var sv in ds)
					{
						context.SinhViens.Add(sv);
					}
					context.SaveChanges();

				}
				ViewBag.Message = "File Uploaded Successfully!!";
				return RedirectToAction("Index");
			}
			catch
			{
				ViewBag.Message = "File upload failed!!";
				return RedirectToAction("Index");
			}
		}
		// GET: SinhVien/Create
		public ActionResult Create()
		{
			var context = new DBSinhVienContext();
			var chucVuSelect = new SelectList(context.ChucVus, "Id", "TenChucVu");
			ViewBag.IdChucVu = chucVuSelect;
			return View();
		}

		// POST: SinhVien/Create
		[HttpPost]
		public ActionResult Create(SinhVien model)
		{
			try
			{
				// TODO: Add insert logic here
				var context = new DBSinhVienContext();
				context.SinhViens.Add(model);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: SinhVien/Edit/5
		public ActionResult Edit(int id)
		{
			var context = new DBSinhVienContext();
			var editing = context.SinhViens.Find(id);
			var chucVuSelect = new SelectList(context.ChucVus, "Id", "TenChucVu", editing.IdChucVu);
			ViewBag.IdChucVu = chucVuSelect;
			return View(editing);
		}

		// POST: SinhVien/Edit/5
		[HttpPost]
		public ActionResult Edit(SinhVien model)
		{
			try
			{
				// TODO: Add update logic here
				var context = new DBSinhVienContext();
				var oldItem = context.SinhViens.Find(model.Id);
				oldItem.HoVaTen = model.HoVaTen;
				oldItem.GioiTinh = model.GioiTinh;
				oldItem.Email = model.Email;
				oldItem.SoDienThoai = model.SoDienThoai;
				oldItem.MaSoSV = model.MaSoSV;
				oldItem.IdChucVu = model.IdChucVu;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: SinhVien/Delete/5
		public ActionResult Delete(int id)
		{
			var context = new DBSinhVienContext();
			var deleting = context.SinhViens.Find(id);
			return View(deleting);
		}

		// POST: SinhVien/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				var context = new DBSinhVienContext();
				var deleting = context.SinhViens.Find(id);
				context.SinhViens.Remove(deleting);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		private bool CheckExcelFile(string fileName)
		{
			return fileName.Contains("xlsx");
		}
		private DataTable ImportExcelAsDataTable(string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			// Mo file excel
			ExcelPackage package = new ExcelPackage(fileInfo);
			// Mo worksheet dau tien
			ExcelWorksheet sheet = package.Workbook.Worksheets.FirstOrDefault();
			// Tao bang du lieu
			DataTable dt = new DataTable();
			// Lay ten thuoc tinh
			foreach (ExcelRangeBase firstRowCell in sheet.Cells[1, 1, 1, sheet.Dimension.End.Column])
			{
				dt.Columns.Add(firstRowCell.Text);
			}
			// Lay du lieu
			for (int rowNum = 2; rowNum <= sheet.Dimension.End.Row; rowNum++)
			{
				ExcelRange row = sheet.Cells[rowNum, 1, rowNum, sheet.Dimension.End.Column];
				DataRow dr = dt.Rows.Add();
				foreach (ExcelRangeBase cell in row)
				{
					dr[cell.Start.Column - 1] = cell.Text;
				}
			}
			package.Dispose();
			return dt;
		}

		private List<SinhVien> ConvertDataTableToList(DataTable dt)
		{
			List<SinhVien> list = new List<SinhVien>();
			try
			{
				foreach (DataRow row in dt.Rows)
				{
					SinhVien sinhVien = new SinhVien
					{
						HoVaTen = row["HoVaTen"].ToString(),
						GioiTinh = row["GioiTinh"].ToString() == "1",
						Email = row["Email"].ToString(),
						SoDienThoai = row["SoDienThoai"].ToString(),
						MaSoSV = row["MaSoSV"].ToString(),
						IdChucVu = long.Parse(row["IdChucVu"].ToString())
					};
					list.Add(sinhVien);
				}
			}
			catch (Exception)
			{
				return null;
			}
			return list;
		}
	}
}
