using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bills
{
    class Program
    {
        private static DateTime SeedPayDate = new DateTime(2016,6,3);
        private static decimal PayAmount = 1454;
        static void Main(string[] args)
        {
            var menu = @"Enter a date or say Hello";
            while (true)
            {
                Console.WriteLine(menu);
                var input = Console.ReadLine();
                switch (input)
                {
                    case "q":
                    case "quit":
                        return;
                    case "Hello":
                    case "hello":
                        Console.WriteLine("Hi");
                        break;
                    default:
                        HandleInput(input);
                        break;
                }
            }
        }
        static void HandleInput(string input)
        {
            DateTime date;
            var isDate = DateTime.TryParse(input, out date);
            if (!isDate)
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            date = date.Date;
            Console.WriteLine("Calculating Bills...");
            CalculateBills(date);
        }
        static void CalculateBills(DateTime date)
        {
            var nextPayDay = NextPayDay(date).Date;
            var lastPayDay = LastPayDay(date).Date;
            //dynamic bills = Bills().SortBills();

            //var bills = Bills().Where(x => x.IsDue(lastPayDay, nextPayDay));

            var bills = Bills();
            Console.WriteLine("Last Pay day was:" + lastPayDay.ToShortDateString());
            Console.WriteLine("Next Pay day is :" + nextPayDay.ToShortDateString());
            Console.WriteLine("Bills due this pay period total to: $" + bills.Where(x => x.NextDueDate(date) < nextPayDay).Sum(x => x.Amount));
            foreach(var bill in bills)
            {
                var dueDate = bill.NextDueDate(date);
                if (dueDate < nextPayDay)
                {
                    Console.WriteLine(bill.Name + " is next due on " + bill.NextDueDate(date).ToShortDateString());
                }


                //var due = bill.IsDue(nextPayDay, lastPayDay);
                //if (due)
                //{
                //    Console.WriteLine(bill.Name + " Is due on " + bill.DayOfMonth );
                //}
                //if (!due)
                //{
                //    Console.WriteLine(bill.Name + " Is not due. Day due is " + bill.DayOfMonth);
                //}
            }

        }
        static List<Bill> Bills()
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
    public static class Extensions
    {
        public static List<Bill> WhereBillsAreDue(this List<Bill> bills, DateTime lastPayday, DateTime nextPayDay)
        {
            var billsDue = new List<Bill>();
            foreach (var bill in bills)
            {
                switch (bill.PayType)
                {
                    case PaymentType.DayOfMonth:
                        break;
                    case PaymentType.DayOfWeek:
                        break;
                }
            }
            return new List<Bill>();
        }

        public static object SortBills(this List<Bill> bills)
        {
            return new { FutureBills = "", PastBills = "" };
        }
    }
    public class Bill
    {
        #region Properties
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PayType { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime SeedDate { get; set; }
        public DayOfWeek Day { get; set; }
        public PaymentInterval Interval { get; set; }
        #endregion

        #region Constructors
        public Bill(string name, decimal amount, PaymentType payType, int day)
    {
        Name = name;
        Amount = amount;
        PayType = payType;
        DayOfMonth = day;
    }
        public Bill(string name, decimal amount, PaymentType payType, DayOfWeek day, PaymentInterval interval)
    {
        Name = name;
        Amount = amount;
        PayType = payType;
        Day = day;
        Interval = interval;
    }
        public Bill(string name, decimal amount, PaymentType payType, DayOfWeek day, PaymentInterval interval, DateTime seedDate)
        {
            Name = name;
            Amount = amount;
            PayType = payType;
            Day = day;
            Interval = interval;
            SeedDate = seedDate;
        }
        #endregion

        #region Public Methods
        public bool IsDue(DateTime prevDate, DateTime nextDate)
        {
            var prevMonth = prevDate.Month;
            var nextMonth = nextDate.Month;

            var prevDay = prevDate.Day;
            var nextDay = nextDate.Day;

            DateTime dueDate;
            if(prevMonth == nextMonth)
            {
                dueDate = new DateTime(nextDate.Year, nextDate.Month, DayOfMonth);
            }

            if (prevDay < DayOfMonth && DayOfMonth <= nextDay)
            {
                return true;
            }
            return false;
        }
        public DateTime NextDueDate(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;

            if(PayType == PaymentType.DayOfWeek)
            {
                var days = (date - SeedDate).Days;
                var i = (double)Interval;
                return date.AddDays(i - (days % i));
            }

            if(DayOfMonth >= day)
            {
                //Bill Due Date is the same month as date
                return new DateTime(date.Year, date.Month, DayOfMonth);
            }

            //If month is december then bill is due in the next year
            if(month == 12)
            {
                return new DateTime(date.Year + 1, 1, DayOfMonth);
            }

            return new DateTime(date.Year, month + 1, DayOfMonth);
        }
        #endregion

        #region Private Methods

        #endregion
    }
    public enum PaymentType
    {
        DayOfMonth,
        DayOfWeek
    }
    public enum PaymentInterval
    {
        Weekly = 7,
        BiWeekly = 14,
        Monthly
    }
}
