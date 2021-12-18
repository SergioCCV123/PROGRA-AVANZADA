using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPulperia;

namespace ProyectoPulperia.Controllers
{
    public class CarritoController : Controller
    {
        private db_a7ddbd_pulperiaEntities db = new db_a7ddbd_pulperiaEntities();

        // GET: Carrito
        public ActionResult Index()
        {
            var carrito = db.Carrito.Include(c => c.Producto).Include(c => c.AspNetUsers);
            return View(carrito.ToList());
        }

        // GET: Carrito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // GET: Carrito/Create
        public ActionResult Create(int? id)
        {
            Producto producto = db.Producto.Find(id);
            String result = GetID();
            AspNetUsers usuario = db.AspNetUsers.Find(result);
            //se necesita traer el id del usuario para que funcione, el user.identity.name trae el nombre de usuario, no el id
            //AspNetUsers usuario = db.AspNetUsers.Find("c527f6e7-4d32-45b2-8cce-c7c2621c577a");



            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO",producto.IDPRODUCTO);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email",usuario.Id);
            return View();
        }

        // POST: Carrito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCARRITO,IDPRODUCTO,USERID,CANTIDADCARRITO")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                db.Carrito.Add(carrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", carrito.IDPRODUCTO);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", carrito.USERID);
            return View(carrito);
        }

        // GET: Carrito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", carrito.IDPRODUCTO);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", carrito.USERID);
            return View(carrito);
        }

        // POST: Carrito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCARRITO,IDPRODUCTO,USERID,CANTIDADCARRITO")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPRODUCTO = new SelectList(db.Producto, "IDPRODUCTO", "NOMBREPRODUCTO", carrito.IDPRODUCTO);
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", carrito.USERID);
            return View(carrito);
        }

        // GET: Carrito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrito carrito = db.Carrito.Find(id);
            db.Carrito.Remove(carrito);
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

        public string GetID ()
        {
            String result = "";
            String username = User.Identity.Name;
            SqlConnection conn = new SqlConnection("Data Source=SQL5108.site4now.net;initial catalog=db_a7ddbd_pulperia;user id=db_a7ddbd_pulperia_admin;password=Pulperia01*;MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                SqlCommand query = new SqlCommand("SELECT Id FROM AspNetUsers WHERE UserName=@username", conn);
                query.Parameters.AddWithValue("@username", username);
                using(SqlDataReader reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader["Id"].ToString();
                    }
                }
            }catch(Exception err)
            {
                Console.WriteLine(err);
                return err.ToString();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
