using System;

namespace myBills.web.Models
{
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
            if (prevMonth == nextMonth)
            {
                dueDate = new DateTime(nextDate.Year, nextDate.Month, DayOfMonth);
            }

            if (prevDay < DayOfMonth && DayOfMonth <= nextDay)
            {
                return true;
            }
            return false;
        }

        public bool IsDue(DateTime date)
        {
            return NextDueDate(DateTime.Now) < date;
        }

        public bool IsDue() { return false; }

        public DateTime NextDueDate(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;

            if (PayType == PaymentType.DayOfWeek)
            {
                var days = (date - SeedDate).Days;
                var i = (double)Interval;
                var nextDate = date.AddDays(i - (days % i));
                return nextDate == date.AddDays(i) ? date : nextDate;
            }

            if (DayOfMonth >= day)
            {
                //Bill Due Date is the same month as date
                return new DateTime(date.Year, date.Month, DayOfMonth);
            }

            //If month is december then bill is due in the next year
            if (month == 12)
            {
                return new DateTime(date.Year + 1, 1, DayOfMonth);
            }

            return new DateTime(date.Year, month + 1, DayOfMonth);
        }

        public DateTime NextDueDate()
        {
            return NextDueDate(DateTime.Now);
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
