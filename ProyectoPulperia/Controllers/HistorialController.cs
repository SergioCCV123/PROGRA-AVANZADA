using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPulperia;

namespace ProyectoPulperia.Controllers
{
    public class HistorialController : Controller
    {
        private db_a7ddbd_pulperiaEntities db = new db_a7ddbd_pulperiaEntities();

        // GET: Historial
        public ActionResult Index()
        {
            var historial = db.Historial.Include(h => h.Compra).Include(h => h.AspNetUsers);
            return View(historial.ToList());
        }

        // GET: Historial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // GET: Historial/Create
        public ActionResult Create()
        {
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID");
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Historial/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHISTORIAL,USERID,IDCOMPRA")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Historial.Add(historial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", historial.IDCOMPRA);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", historial.USERID);
            return View(historial);
        }

        // GET: Historial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", historial.IDCOMPRA);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", historial.USERID);
            return View(historial);
        }

        // POST: Historial/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHISTORIAL,USERID,IDCOMPRA")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", historial.IDCOMPRA);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", historial.USERID);
            return View(historial);
        }

        // GET: Historial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // POST: Historial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Historial historial = db.Historial.Find(id);
            db.Historial.Remove(historial);
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
