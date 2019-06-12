using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS.DAL;
using POS.Models;

namespace POS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult Detail_Customer_Index()
        {
            return View();
        }

        public ActionResult Detail_Customer() {

            using (PosContext db = new PosContext())
            {
                List<Customer> custlist = db.Customers.ToList<Customer>();
                return Json(new { data = custlist }, JsonRequestBehavior.AllowGet);


            }
        }

        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else
            {
                using (PosContext db = new PosContext())
                {

                    return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault<Customer>());
                }


            }
        }

        [HttpPost]
        public ActionResult AddorEdit(Customer cust)//we have to pass instance of object in defined stages of enity that's we have created instance 
        {
            var CustomerId = new SqlParameter("CustomerId", SqlDbType.Int);
            CustomerId.Value = cust.CustomerId;
            var CustomerName = new SqlParameter("CustomerName",SqlDbType.NVarChar);
            CustomerName.Value = cust.CustomerName;
            var CustomerAddress = new SqlParameter("CustomerAddress", SqlDbType.NVarChar);
            CustomerAddress.Value = cust.CustomerAddress;
            var CustomerGender = new SqlParameter("Gender", SqlDbType.NVarChar);
            CustomerGender.Value = cust.Gender;
            using (PosContext db = new PosContext())
                {
                if (cust.CustomerId == 0 && ModelState.IsValid)
                {
                    /*By using Entity Framework
                     db.Customers.Add(cust);*/
                    //By using Stored Procedure
                    db.Database.ExecuteSqlCommand("exec Customer_Insert @CustomerName,@CustomerAddress,@Gender", CustomerName, CustomerAddress, CustomerGender);
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else if (cust.CustomerId != 0 && ModelState.IsValid)
                {
                    /*By using Entity framework
                     db.Entry(cust).State = EntityState.Modified;*/
                    //By using Stored Procedure
                    db.Database.ExecuteSqlCommand("exec Customer_Update  @CustomerId,@CustomerName,@CustomerAddress,@Gender", CustomerId, CustomerName, CustomerAddress, CustomerGender);
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("CustomerName","Name is Required");
                    return View();

                }
                else
                {

                    return View(cust);
                }
               

                }
            }
           

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var CustomerId = new SqlParameter("CustomerId", SqlDbType.Int);
            CustomerId.Value = id;
            using (PosContext db = new PosContext())
            {
                /* By using Entity Framework
                Customer cust = db.Customers.Where(x => x.CustomerId == id).FirstOrDefault<Customer>();
                db.Customers.Remove(cust);*/
                
                //By Using stored Procedure 
                db.Database.ExecuteSqlCommand("exec Customer_Delete  @CustomerId", CustomerId);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail_Supplier_Index()
        {

            return View();
        }

        public ActionResult Detail_Supplier()
        {

            using (PosContext db = new PosContext())
            {
                List<Supplier> supplist = db.Suppliers.ToList<Supplier>();
                return Json(new { data = supplist }, JsonRequestBehavior.AllowGet);


            }
        }

        [HttpGet]
        public ActionResult AddorEdit_Supplier(int id = 0)
        {

            if (id == 0)
            {
                return View(new Supplier());
            }
            else
            {
                using (PosContext db = new PosContext())
                {

                    return View(db.Suppliers.Where(x => x.SupplierId == id).FirstOrDefault<Supplier>());
                }


            }
        }

        [HttpPost]
        public ActionResult AddorEdit_Supplier(Supplier supp)//we have to pass instance of object in defined stages of enity that's we have created instance 
        {
            using (PosContext db = new PosContext())
            {
                if (supp.SupplierId == 0)
                {
                    db.Suppliers.Add(supp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(supp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);


                }
            }
        }

        [HttpPost]
        public ActionResult Delete_Supplier(int id)
        {
            using (PosContext db = new PosContext())
            {
                Supplier supp = db.Suppliers.Where(x => x.SupplierId == id).FirstOrDefault<Supplier>();
                db.Suppliers.Remove(supp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail_Product_Index()
        {

            return View();
        }

        public ActionResult Detail_Product()
        {

            using (PosContext db = new PosContext())
            {
                List<Product> prodlist = db.Products.ToList<Product>();
                return Json(new { data = prodlist }, JsonRequestBehavior.AllowGet);


            }
        }

        [HttpGet]
        public ActionResult AddorEdit_Product(int id = 0)
        {

            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                using (PosContext db = new PosContext())
                {

                    return View(db.Products.Where(x => x.ProductId == id).FirstOrDefault<Product>());
                }


            }
        }

        [HttpPost]
        public ActionResult AddorEdit_Product(Product prod)//we have to pass instance of object in defined stages of enity that's we have created instance 
        {
            using (PosContext db = new PosContext())
            {
                if (prod.ProductId == 0)
                {
                    db.Products.Add(prod);
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(prod).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);


                }
            }
        }

        [HttpPost]
        public ActionResult Delete_Product(int id)
        {
            using (PosContext db = new PosContext())
            {
                Product prod = db.Products.Where(x => x.ProductId == id).FirstOrDefault<Product>();
                db.Products.Remove(prod);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}