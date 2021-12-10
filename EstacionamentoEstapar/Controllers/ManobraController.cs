using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EstacionamentoEstapar.Models;

namespace EstacionamentoEstapar.Controllers
{
    public class ManobraController : Controller
    {
        private EstaparEntities db = new EstaparEntities();

        // GET: Manobra
        public ActionResult Index()
        {
            var manobras = db.Manobras.Include(m => m.Manobrista).Include(m => m.Modelo);
            return View(manobras.ToList());
        }

        // GET: Manobra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobra manobra = db.Manobras.Find(id);
            if (manobra == null)
            {
                return HttpNotFound();
            }
            return View(manobra);
        }

        // GET: Manobra/Create
        public ActionResult Create()
        {
            ViewBag.IdManobrista = new SelectList(db.Manobristas, "IdManobrista", "Nome");
            ViewBag.IdModelo = new SelectList(db.Modeloes, "IdModelo", "DscModelo");
            return View();
        }

        // POST: Manobra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdManobra,IdModelo,DscPlaca,IdManobrista")] Manobra manobra)
        {
            if (ModelState.IsValid)
            {
                db.Manobras.Add(manobra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdManobrista = new SelectList(db.Manobristas, "IdManobrista", "Nome", manobra.IdManobrista);
            ViewBag.IdModelo = new SelectList(db.Modeloes, "IdModelo", "DscModelo", manobra.IdModelo);
            return View(manobra);
        }

        // GET: Manobra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobra manobra = db.Manobras.Find(id);
            if (manobra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdManobrista = new SelectList(db.Manobristas, "IdManobrista", "Nome", manobra.IdManobrista);
            ViewBag.IdModelo = new SelectList(db.Modeloes, "IdModelo", "DscModelo", manobra.IdModelo);
            return View(manobra);
        }

        // POST: Manobra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdManobra,IdModelo,DscPlaca,IdManobrista")] Manobra manobra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manobra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdManobrista = new SelectList(db.Manobristas, "IdManobrista", "Nome", manobra.IdManobrista);
            ViewBag.IdModelo = new SelectList(db.Modeloes, "IdModelo", "DscModelo", manobra.IdModelo);
            return View(manobra);
        }

        // GET: Manobra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobra manobra = db.Manobras.Find(id);
            if (manobra == null)
            {
                return HttpNotFound();
            }
            return View(manobra);
        }

        // POST: Manobra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manobra manobra = db.Manobras.Find(id);
            db.Manobras.Remove(manobra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
