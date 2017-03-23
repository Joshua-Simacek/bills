using System;
using System.Collections.Generic;
using myBills.web.Data.Dapper;
using myBills.web.Data.LinqToSql;
using myBills.web.Models;

namespace myBills.web.Data
{
    public class BillData
    {
        public IEnumerable<Bill> GetBills()
        {
            using (var dbCtx = new BillDataDataContext())
            {
                var bills = dbCtx.bills;
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

        /*
        SELECT[name]
          ,[amount]
          ,[pay_type]
          ,[day_of_month]
          ,[seed_date]
          ,[day_of_week]
          ,[pay_interval]
        FROM[dbo].[bills]
        */

        public int AddBill(BillDto bill)
        {

            if(bill.interval == 'm')
            {
                var sql = "INSERT INTO [dbo].[Bills]([name],[amount],[pay_type],[day_of_month])" +
                          "VALUES(@name, @amount, @pay_type, @day_of_month)";
                var newBill = new { name = bill.name, amount = bill.amount, pay_type = "m", day_of_month = bill.dayofmonth };
                return DapperUtils.InsertItem(sql, newBill);
            }

            if(bill.interval == 'b')
            {
                var sql = "INSERT INTO [dbo].[Bills]([name],[amount],[pay_type],[seed_date],[day_of_week],[pay_interval])" +
                          "VALUES(@name, @pay_type, @seed_date, @day_of_week, @pay_interval)";
                var newBill = new { name = bill.name, amount = bill.amount, pay_type = "w", seed_date = bill.seedpayday, day_of_week = bill.dayofweek, pay_interval = "b" };
                return DapperUtils.InsertItem(sql, newBill);
            }

            return 0;
        }

        public int DeleteBill(string Name)
        {
            var sql = "DELETE FROM [dbo].[Bills] WHERE [name] = @key";
            return DapperUtils.DeleteItem(sql, Name);
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
