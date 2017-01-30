using myBills.web.Data.LinqToSql;
using myBills.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myBills.web.Data
{
    public class BillData
    {
        public IEnumerable<Bill> GetBills()
        {
            using (var dbCtx = new BillDataDataContext())
            {
                var bills = dbCtx.bills.AsEnumerable();
                foreach (var bill in bills)
                {
                    switch (bill.pay_type)
                    {
                        case 'm':
                            yield return new Bill(bill.name, bill.amount.Value, PaymentType.DayOfMonth, (int)bill.day_of_month);
                            break;
                        case 'w':
                            yield return new Bill(bill.name, bill.amount.Value, PaymentType.DayOfWeek, (DayOfWeek)bill.day_of_week, GetPaymentInterval(bill.pay_interval.Value));
                            break;
                    }
                }
            }
        }

        private PaymentInterval GetPaymentInterval(char pay_interval)
        {
            switch(pay_interval)
            {
                case 'w':
                    return PaymentInterval.Weekly;
                case 'b':
                    return PaymentInterval.BiWeekly;
                case 'm':
                default:
                    return PaymentInterval.Monthly;
            }
        }

    }
}
