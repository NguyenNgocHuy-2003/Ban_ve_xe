﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ban_ve_xe.Models;
using System.Data.Entity;
using System.Data;

namespace Ban_ve_xe.Controllers
{
    public class TrafficController : BaseController
    {
        private DB_BUSEntities db = new DB_BUSEntities();
        //
        // GET: /Traffic/
        public ActionResult Index()
        {
            return View(db.LuuThongs.Include(x => x.TuyenXe).Include(x => x.PhuXe).Include(x => x.TaiXe).Include(x => x.XeBuyt).ToList());
        }

        public ActionResult Print()
        {
            return View(db.LuuThongs.Include(x => x.TuyenXe).Include(x => x.PhuXe).Include(x => x.TaiXe).Include(x => x.XeBuyt).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var busroute = db.TuyenXes.ToList();
            ViewBag.MaTuyenXe = new SelectList(busroute, "MaTuyenXe", "TenTuyenXe");
            var bus = db.XeBuyts.ToList();
            ViewBag.MaXeBuyt = new SelectList(bus, "MaXeBuyt", "BienKiemSoat");
            var driver = db.TaiXes.ToList();
            ViewBag.MaTaiXe = new SelectList(driver, "MaTaiXe", "HoTen");
            var assistant = db.PhuXes.ToList();
            ViewBag.MaPhuXe = new SelectList(assistant, "MaPhuXe", "HoTen");
            return View();
        }

        [HttpPost]
        public ActionResult Create(LuuThong e)
        {
            e.Ngay = DateTime.Now;
            db.LuuThongs.Add(e);
            db.SaveChanges();
            TempData["ThongBao"] = "Them moi thanh cong!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.XeBuyt = db.XeBuyts.ToList();
            ViewBag.TuyenXe = db.TuyenXes.ToList();
            ViewBag.TaiXe = db.TaiXes.ToList();
            ViewBag.PhuXe = db.PhuXes.ToList();
            return View(db.LuuThongs.SingleOrDefault(x => x.MaLuuThong == id));
        }

        [HttpPost]
        public ActionResult Edit(int id, LuuThong traffic)
        {
            var e = db.LuuThongs.SingleOrDefault(x => x.MaLuuThong == id);
            e.Ngay = traffic.Ngay;
            e.Ca = traffic.Ca;
            e.MaPhuXe = traffic.MaPhuXe;
            e.MaTaiXe = traffic.MaTaiXe;
            e.MaTuyenXe = traffic.MaTuyenXe;
            e.MaXeBuyt = traffic.MaXeBuyt;
            db.SaveChanges();
            TempData["ThongBao"] = "Sua thanh cong!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var e = db.LuuThongs.SingleOrDefault(x => x.MaLuuThong == id);
                db.LuuThongs.Remove(e);
                db.SaveChanges();
            }
            catch
            {
                TempData["ThongBao"] = "Xoa khong thanh cong!";
            }
            TempData["ThongBao"] = "Xoa thanh cong!";
            return RedirectToAction("Index");
        }

    }
}