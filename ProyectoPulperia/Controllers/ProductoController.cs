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
    public class ProductoController : Controller
    {
        private db_a7ddbd_pulperiaEntities db = new db_a7ddbd_pulperiaEntities();

        // GET: Producto
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Marca);
            return View(producto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.IDCATEGORIAPRODUCTO = new SelectList(db.Categoria, "IDCATEGORIA", "NOMBRECATEGORIA");
            ViewBag.IDMARCAPRODUCTO = new SelectList(db.Marca, "IDMARCA", "NOMBREMARCA");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPRODUCTO,NOMBREPRODUCTO,PRECIOPRODUCTO,IDCATEGORIAPRODUCTO,IDMARCAPRODUCTO")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCATEGORIAPRODUCTO = new SelectList(db.Categoria, "IDCATEGORIA", "NOMBRECATEGORIA", producto.IDCATEGORIAPRODUCTO);
            ViewBag.IDMARCAPRODUCTO = new SelectList(db.Marca, "IDMARCA", "NOMBREMARCA", producto.IDMARCAPRODUCTO);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCATEGORIAPRODUCTO = new SelectList(db.Categoria, "IDCATEGORIA", "NOMBRECATEGORIA", producto.IDCATEGORIAPRODUCTO);
            ViewBag.IDMARCAPRODUCTO = new SelectList(db.Marca, "IDMARCA", "NOMBREMARCA", producto.IDMARCAPRODUCTO);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPRODUCTO,NOMBREPRODUCTO,PRECIOPRODUCTO,IDCATEGORIAPRODUCTO,IDMARCAPRODUCTO")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCATEGORIAPRODUCTO = new SelectList(db.Categoria, "IDCATEGORIA", "NOMBRECATEGORIA", producto.IDCATEGORIAPRODUCTO);
            ViewBag.IDMARCAPRODUCTO = new SelectList(db.Marca, "IDMARCA", "NOMBREMARCA", producto.IDMARCAPRODUCTO);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
