using myBills.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBills.web.Controllers
{
    public class BillsController : Controller
    {
        private static DateTime SeedPayDate = new DateTime(2016, 6, 3);
        // GET: Bills
        [HttpGet]
        public ActionResult Index()
        {
            var model = new BillsViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DateTime Date)
        {
            var model = new BillsViewModel(Date);
            return View(model);
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