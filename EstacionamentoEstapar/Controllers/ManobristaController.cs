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
    public class ManobristaController : Controller
    {
        private EstaparEntities db = new EstaparEntities();

        // GET: Manobrista
        public ActionResult Index()
        {
            return View(db.Manobristas.ToList());
        }

        // GET: Manobrista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobrista manobrista = db.Manobristas.Find(id);
            if (manobrista == null)
            {
                return HttpNotFound();
            }
            return View(manobrista);
        }

        // GET: Manobrista/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manobrista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdManobrista,Nome,CPF,DtNascimento")] Manobrista manobrista)
        {
            if (ModelState.IsValid)
            {
                db.Manobristas.Add(manobrista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manobrista);
        }

        // GET: Manobrista/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobrista manobrista = db.Manobristas.Find(id);
            if (manobrista == null)
            {
                return HttpNotFound();
            }
            return View(manobrista);
        }

        // POST: Manobrista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdManobrista,Nome,CPF,DtNascimento")] Manobrista manobrista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manobrista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manobrista);
        }

        // GET: Manobrista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manobrista manobrista = db.Manobristas.Find(id);
            if (manobrista == null)
            {
                return HttpNotFound();
            }
            return View(manobrista);
        }

        // POST: Manobrista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manobrista manobrista = db.Manobristas.Find(id);

            if (db.Manobras.FirstOrDefault(x => x.IdManobrista == id) != null)
            {
                TempData["alertMessage"] = "Antes de excluir o manobrista '" + manobrista.Nome + "' favor excluir primeiro as manobras cadastradas com o mesmo.";
                return RedirectToAction("Delete", id);
            }
            else
            {
                db.Manobristas.Remove(manobrista);
                db.SaveChanges();
            }
     
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
