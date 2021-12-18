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
    public class DetalleCompraController : Controller
    {
        private db_a7ddbd_pulperiaEntities db = new db_a7ddbd_pulperiaEntities();

        // GET: DetalleCompra
        public ActionResult Index()
        {
            var detalleCompra = db.DetalleCompra.Include(d => d.Compra).Include(d => d.Producto);
            return View(detalleCompra.ToList());
        }

        // GET: DetalleCompra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // GET: DetalleCompra/Create
        public ActionResult Create()
        {
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID");
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO");
            return View();
        }

        // POST: DetalleCompra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDETALLECOMPRA,IDCOMPRA,IDPRODUCTO,CANTIDADDC,PRECIODC")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.DetalleCompra.Add(detalleCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", detalleCompra.IDCOMPRA);
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", detalleCompra.IDPRODUCTO);
            return View(detalleCompra);
        }

        // GET: DetalleCompra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", detalleCompra.IDCOMPRA);
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", detalleCompra.IDPRODUCTO);
            return View(detalleCompra);
        }

        // POST: DetalleCompra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDETALLECOMPRA,IDCOMPRA,IDPRODUCTO,CANTIDADDC,PRECIODC")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCOMPRA = new SelectList(db.Compra, "IDCOMPRA", "USERID", detalleCompra.IDCOMPRA);
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", detalleCompra.IDPRODUCTO);
            return View(detalleCompra);
        }

        // GET: DetalleCompra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // POST: DetalleCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            db.DetalleCompra.Remove(detalleCompra);
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
