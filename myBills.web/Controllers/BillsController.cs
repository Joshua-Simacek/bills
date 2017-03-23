using System;
using System.Web.Mvc;
using myBills.web.Data;
using myBills.web.Models;

namespace myBills.web.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private static DateTime SeedPayDate = new DateTime(2016, 6, 3);
        // GET: Bills
        [HttpGet]
        public ActionResult Index()
        {
            return View(new BillsViewModel());
        }

        //[HttpPost]
        //public ActionResult Index(DateTime Date)
        //{
        //    var model = new BillsViewModel(Date);
        //    return View(model);
        //}

        public ActionResult GetBills(DateTime? Date)
        {
            var date = Date ?? DateTime.Now;
            var model = new BillsViewModel(date);
            return PartialView("_BillsTable", model);
        }

        [HttpPost]
        public ActionResult AddBill(BillDto bill)
        {
            var result = ((new BillData().AddBill(bill) > 0) ? "New Bill Saved" : "Failed to save new bill");
            return GetBills(null);
            //return PartialView("_BillsTable", new BillsViewModel());
        }

        public ActionResult DeleteBill(string Name)
        {
            var result = (new BillData().DeleteBill(Name) > 0 ? "Bill Deleted" : "Failed to delete bill");
            return GetBills(null);
        }

        static DateTime NextPayDay(DateTime date, PaymentInterval paymentInterval = PaymentInterval.BiWeekly)
        {
            var days = (date - SeedPayDate).Days;
            var i = (double)paymentInterval;
            return date.Date.AddDays(i - (days % i));
        }
        static DateTime LastPayDay(DateTime date, PaymentInterval paymentInterval = PaymentInterval.BiWeekly)
        {
            var days = (date - SeedPayDate).Days;
            var i = (double)paymentInterval;
            return date.Date.AddDays((-1) * (days % i));
        }

    }
}