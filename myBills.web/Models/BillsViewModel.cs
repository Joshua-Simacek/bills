using System;
using System.Collections.Generic;
using System.Linq;
using myBills.web.Data;

namespace myBills.web.Models
{
    public class BillsViewModel
    {
        private static DateTime SeedPayDate;
        private static DateTime date;
        private IEnumerable<Bill> bills;

        public DateTime NextPayDay { get { return nextPayDay(date); } }
        public DateTime LastPayDay { get { return lastPayDay(date); } }
        public decimal Total { get { return bills.Sum(x => x.Amount); } }
        public decimal TotalDue { get { return bills.Where(x => x.NextDueDate(LastPayDay) < NextPayDay).Sum(x => x.Amount); } }
        public List<Bill> Bills { get { return bills.ToList(); } }


        #region constructors
        public BillsViewModel()
        {
            bills = new BillData().GetBills();
            SeedPayDate = new DateTime(2016, 6, 3);
            date = DateTime.Now;
        }

        public BillsViewModel(DateTime today)
        {
            bills = new BillData().GetBills();
            SeedPayDate = new DateTime(2016, 6, 3);
            date = today;
        }

        public BillsViewModel(DateTime today, DateTime seedPayDate)
        {
            bills = new BillData().GetBills();
            date = today;
            SeedPayDate = seedPayDate;
        }
        #endregion

        static DateTime nextPayDay(DateTime date, PaymentInterval paymentInterval = PaymentInterval.BiWeekly)
        {
            var days = (date - SeedPayDate).Days;
            var i = (double)paymentInterval;
            return date.Date.AddDays(i - (days % i));
        }
        static DateTime lastPayDay(DateTime date, PaymentInterval paymentInterval = PaymentInterval.BiWeekly)
        {
            var days = (date - SeedPayDate).Days;
            var i = (double)paymentInterval;
            return date.Date.AddDays((-1) * (days % i));
        }
        static List<Bill> GetBills()
        {
            return new List<Bill>()
            {
                new Bill("Rent", 500, PaymentType.DayOfMonth, 1),
                new Bill("Dance", 45 , PaymentType.DayOfMonth, 1),
                new Bill("Gymnastics", 100, PaymentType.DayOfMonth, 1),
                new Bill("Age of Learning", 8, PaymentType.DayOfMonth, 2),
                new Bill("Amazon Store Card", 50, PaymentType.DayOfMonth, 5),
                new Bill("Amazon Rewards Card", 25, PaymentType.DayOfMonth, 6),
                new Bill("Electricity", 160, PaymentType.DayOfMonth, 6),
                new Bill("Netflix", 8, PaymentType.DayOfMonth, 7),
                new Bill("Ciara's Phone", 48, PaymentType.DayOfMonth, 25),
                new Bill("Care Credit Card", 40, PaymentType.DayOfMonth, 10),
                new Bill("Maurice's Card", 27, PaymentType.DayOfMonth, 11),
                new Bill("Court Fines", 200, PaymentType.DayOfWeek, DayOfWeek.Friday, PaymentInterval.BiWeekly, SeedPayDate),
                new Bill("Dentist", 100, PaymentType.DayOfWeek, DayOfWeek.Friday, PaymentInterval.BiWeekly, SeedPayDate),
                new Bill("Comcast", 128, PaymentType.DayOfMonth, 15),
                new Bill("Google Music", 10, PaymentType.DayOfMonth, 17),
                new Bill("Water", 70, PaymentType.DayOfMonth, 20),
                new Bill("Hulu", 8, PaymentType.DayOfMonth, 21),
                new Bill("Capital One Card", 31, PaymentType.DayOfMonth, 25),
                new Bill("Joshua's Phone", 48, PaymentType.DayOfMonth, 5),
                new Bill("Best Buy", 27, PaymentType.DayOfMonth, 27),
                new Bill("Humble Bundle", 12, PaymentType.DayOfMonth, 29),
                new Bill("Health Insurance", 37, PaymentType.DayOfMonth, 30)
            };
        }
    }
}
