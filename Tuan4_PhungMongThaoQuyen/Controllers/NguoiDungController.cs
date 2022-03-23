using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan4_PhungMongThaoQuyen.Models;

namespace Tuan4_PhungMongThaoQuyen.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["Ho ten"];
            var tendangnhap = collection["Ten dang nhap"];
            var matkhau = collection["Mat khau"];
            var Matkhauxacnhan = collection["Xac Nhan Mat Khau"];
            var email = collection["email"];
            var diachi = collection["Dia chi"];
            var dienthoai = collection["Dien thoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            if (String.IsNullOrEmpty(Matkhauxacnhan))
            {
                ViewData["NhapMKXN"] = " Phải nhập mật khẩu xác nhận !";
            }
            else
            {
                if (!matkhau.Equals(Matkhauxacnhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và xác nhận mật khẩu phải giống nhau";

                }
                else
                {
                    kh.hoten = hoten;
                    kh.tendangnhap = tendangnhap;
                    kh.matkhau = matkhau;
                    kh.email = email;
                    kh.diachi = diachi;
                    kh.dienthoai = dienthoai;
                    kh.ngaysinh = DateTime.Parse(ngaysinh);

                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}