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
    public class CompraController : Controller
    {
        private db_a7ddbd_pulperiaEntities db = new db_a7ddbd_pulperiaEntities();

        // GET: Compra
        public ActionResult Index()
        {
            var compra = db.Compra.Include(c => c.AspNetUsers);
            return View(compra.ToList());
        }

        // GET: Compra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compra/Create
        public ActionResult Create()
        {
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Compra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCOMPRA,USERID,TOTALCOMPRA,FECHACOMPRA")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compra.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", compra.USERID);
            return View(compra);
        }

        // GET: Compra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", compra.USERID);
            return View(compra);
        }

        // POST: Compra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCOMPRA,USERID,TOTALCOMPRA,FECHACOMPRA")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERID = new SelectList(db.AspNetUsers, "Id", "Email", compra.USERID);
            return View(compra);
        }

        // GET: Compra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compra.Find(id);
            db.Compra.Remove(compra);
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

        public void Purchase()
        {
            String username = GetID();
            SqlConnection conn = null;
            try
            {
                SqlCommand query = new SqlCommand("UserPurchase");
                query.CommandType = CommandType.StoredProcedure;

                query.Parameters.AddWithValue("@USER", SqlDbType.NVarChar).Value = username;
                conn = new SqlConnection("Data Source=SQL5108.site4now.net;initial catalog=db_a7ddbd_pulperia;user id=db_a7ddbd_pulperia_admin;password=Pulperia01*;MultipleActiveResultSets=True");
                conn.Open();
                query.Connection = conn;
                query.ExecuteNonQuery();
                
            }catch(Exception err)
            {
                Console.WriteLine(err);
            }
            finally
            {
                conn.Close();
                Index();
            }
        }

        public string GetID()
        {
            String result = "";
            String username = User.Identity.Name;
            SqlConnection conn = new SqlConnection("Data Source=SQL5108.site4now.net;initial catalog=db_a7ddbd_pulperia;user id=db_a7ddbd_pulperia_admin;password=Pulperia01*;MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                SqlCommand query = new SqlCommand("SELECT Id FROM AspNetUsers WHERE UserName=@username", conn);
                query.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader["Id"].ToString();
                    }
                }
            }
            catch (Exception err)
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
