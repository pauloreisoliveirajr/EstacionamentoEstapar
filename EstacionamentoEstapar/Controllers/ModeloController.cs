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
    public class ModeloController : Controller
    {
        private EstaparEntities db = new EstaparEntities();

        // GET: Modelo
        public ActionResult Index()
        {
            var modeloes = db.Modeloes.Include(m => m.Marca);
            return View(modeloes.ToList());
        }

        // GET: Modelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = db.Modeloes.Find(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // GET: Modelo/Create
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "DscMarca");
            return View();
        }

        // POST: Modelo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdModelo,IdMarca,DscModelo")] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                db.Modeloes.Add(modelo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "DscMarca", modelo.IdMarca);
            return View(modelo);
        }

        // GET: Modelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = db.Modeloes.Find(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "DscMarca", modelo.IdMarca);
            return View(modelo);
        }

        // POST: Modelo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdModelo,IdMarca,DscModelo")] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "DscMarca", modelo.IdMarca);
            return View(modelo);
        }

        // GET: Modelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = db.Modeloes.Find(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: Modelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modelo modelo = db.Modeloes.Find(id);

            if (db.Manobras.FirstOrDefault(x => x.IdManobrista == id) != null)
            {
                TempData["alertMessage"] = "Antes de excluir o modelo '" + modelo.DscModelo + "' favor excluir primeiro as manobras cadastradas com o mesmo.";
                return RedirectToAction("Delete", id);
            }
            else
            {
                db.Modeloes.Remove(modelo);
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
