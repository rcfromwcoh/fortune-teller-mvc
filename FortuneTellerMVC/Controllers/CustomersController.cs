using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        public object Viewbag { get; private set; }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.Color);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            string userFullName = customer.FirstName + customer.LastName;

            string firstLetterOfBirthMonth = customer.BirthMonth.BirthMonthName.Substring(0, 1);
            string secondLetterOfBirthMonth = customer.BirthMonth.BirthMonthName.Substring(1, 1);
            string thirdLetterOfBirthMonth = customer.BirthMonth.BirthMonthName.Substring(2, 1);

            int indexFirstLetter = userFullName.IndexOf(firstLetterOfBirthMonth);
            int indexSecondLetter = userFullName.IndexOf(secondLetterOfBirthMonth);
            int indexThirdLetter = userFullName.IndexOf(thirdLetterOfBirthMonth);


            //double moneyInBank = 25000.00;

            if (indexFirstLetter != -1)
            {
                ViewBag.moneyInBank = 8500000.12;
            }
            else if (indexSecondLetter != -1)
            {
                ViewBag.moneyInBank = 26500000.67;
            }
            else if (indexThirdLetter != -1)
            {
                ViewBag.moneyInBank = 978999.54;
            }
            else
            {
                ViewBag.moneyInBank = 35000.00;
            }
            if (customer.Age % 2 == 0)
            {
                //even
                ViewBag.NumberOfYears = 20;
            }
            else
            {
                ViewBag.NumberOfYears = 35;
            }
            //string firstLetterOfRoygbivValue = customer.Color.Substring(0, 1);
            //roygbivValue = firstLetterOfRoygbivValue;

            

            switch (customer.ColorID)
            {
                case 1:
                    ViewBag.modeOfTransportation = "Range Rover";
                    break;

                case 2:
                    ViewBag.modeOfTransportation = "Outback SUV";
                    break;

                case 3:
                    ViewBag.modeOfTransportation = "Yacht";
                    break;

                case 4:
                    ViewBag.modeOfTransportation = "Golf Cart";
                    break;

                case 5:
                    ViewBag.modeOfTransportation = "Boat";
                    break;

                case 6:
                    ViewBag.modeOfTransportation = "Intrepid";
                    break;

                case 7:
                    ViewBag.modeOfTransportation = "Viper Convertible";
                    break;

                default:
                    ViewBag.modeOfTransportation = "Jet Plane";
                    break;
            }
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.userFutureLocation = "Tokyo, Japan";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.userFutureLocation = "London, England";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.userFutureLocation = "Sidney, Australia";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.userFutureLocation = "Miami, Florida";
            }
            else
            {
                ViewBag.userFutureLocation = "Shanghai, China";
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
