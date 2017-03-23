using System;
using System.Linq;

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
            var year = date.Year;

            if (PayType == PaymentType.DayOfWeek)
            {
                var days = (date - SeedDate).Days;
                var i = (double)Interval;
                var nextDate = date.AddDays(i - (days % i));
                return nextDate == date.AddDays(i) ? date : nextDate;
            }

            //If the DayOfMonth that bill is to be payed is before today, then the bill is due next month
            if(DayOfMonth < day)
            {
                month++;

                //If month is december and bill is due next month then bill is due in the next year
                if (month == 12)
                {
                    year++;
                }
            }

            //Handle invalid February Dates
            if (month == 2 && (new[] { 29, 30, 31 }).Contains(DayOfMonth))
            {
                month++;
                DayOfMonth = (DayOfMonth - (DateTime.IsLeapYear(year) ? 29: 28));
            }

            //Handle invalid dates 
            if ((new[] { 4,6,9,11 }).Contains(month) && DayOfMonth == 30)
            {
                month++;
                DayOfMonth = 1;
            }

            return new DateTime(year, month, DayOfMonth);
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
