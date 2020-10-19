using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using company.Models;
using System.IO;

namespace company.Areas.Amincompany.Controllers
{
   
    public class Default1Controller : Controller
    {
        private tshirtsEntities db = new tshirtsEntities();

        // GET: /Amincompany/Default1/
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: /Amincompany/Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Amincompany/Default1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Amincompany/Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProductId,Price,ProductName,whalesaleprice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Amincompany/Default1/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Amincompany/Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductId,Price,ProductName,whalesaleprice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Amincompany/Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Amincompany/Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            var rowscolor = db.Color.Where(x => x.ProductId == id).ToList();
            foreach (var row in rowscolor)
            {
                db.Color.Remove(row);
            
            }
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult viewcolor(int id)
        {
            var color = (from c in (db.Color)
                         where c.ProductId == id
                         select c);

            return View(color.ToList());
        }
       
        public ActionResult Createcolor(int id)
        {
            Color color = new Color();
            var s = Request.Url.PathAndQuery;
            return View(color);
        }

        [HttpPost]
        public ActionResult Createcolor([Bind(Exclude="Img")]Color color,int id, HttpPostedFileBase Img)
        {

            bool status = false;

            tshirtsEntities db =new tshirtsEntities();
           
            //if (Img != null)
            //{
            //    color.Img = new byte [Img.ContentLength];
            //    Img.InputStream.Read(color.Img, 0, Img.ContentLength);
               
            //}
            //db.Color.Add(color);
            //db.SaveChanges();



           
            using (BinaryReader br = new BinaryReader(Img.InputStream))
            {
              color.Img = br.ReadBytes(Img.ContentLength);
            }
            color.ProductId = id;
            db.Color.Add(color);
            db.SaveChanges();
            ViewBag.Status = status;
            return View(color);
        }


        public ActionResult editcolor(int id, int idcolor)
        {
            Color color = new Color();
            var s = Request.Url.PathAndQuery;
            var row = db.Color.Where(x => x.ColorId == idcolor).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult editcolor([Bind(Exclude = "Img")]Color color,  int id, HttpPostedFileBase Img)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var row = db.Color.Where(x => x.ColorId == color.ColorId).FirstOrDefault();
                if (Img != null)
                {
                    using (BinaryReader br = new BinaryReader(Img.InputStream))
                    {
                        color.Img = br.ReadBytes(Img.ContentLength);
                    }
                }
                else
                {
                    color.Img = row.Img;
                }
                color.ProductId = id;
               
                db.Color.Remove(row);
                db.Color.Add(color);
                db.SaveChanges();
                status = true;
                ViewBag.Status = status;
            }
            return View(color);
        }
        
        public ActionResult deletecolor(int id,int idcolor)
        {
           
            Color color = new Color();
            var s = Request.Url.PathAndQuery;
            if (ModelState.IsValid)
            {
                var row = db.Color.Where(x => x.ColorId == idcolor).FirstOrDefault();

                db.Color.Remove(row);
                db.SaveChanges();
                
            }
                return RedirectToAction("viewcolor", "Default1", new { id = id });
            
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










