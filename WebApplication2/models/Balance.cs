using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.models
{
     public class Balance
    {
        public Balance(double in_balance, double calculation, int accountId, string period)
        {
            In_balance = in_balance;
            Calculation = calculation;
            AccountId = accountId;
            Period = new Period(period);
            Out_balance = in_balance + calculation;
            Bills = GetPaid_Bills(accountId).Where(s => s.Date.Year == Period.Year && s.Date.Month == Period.Month).ToList();
            foreach (Paid_bill bill in Bills)
            {
                Out_balance -= bill.Sum;
                Bills_sum += bill.Sum;
            }
            Out_balance = Math.Round(Out_balance, 2);
            Bills_sum = Math.Round(Bills_sum, 2);

        }

        public double In_balance { get; set; }
        public double Calculation  { get; set; }
        public int AccountId  { get; set; }
        public Period Period   { get; set; }
        public double Out_balance { get; set; }
        public List<Paid_bill> Bills { get; set; }
        public double Bills_sum { get; set; } = 0;

        static List<Paid_bill> GetPaid_Bills(int accountId)
        {
            List<Paid_bill> paid_Bills = new List<Paid_bill>();
            var All_bills = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Source/payment_202105270827.json"));
            foreach (var item in All_bills)
            {
                paid_Bills.Add(new Paid_bill(int.Parse(item["account_id"].ToString()), DateTime.Parse(item["date"].ToString()), double.Parse(item["sum"].ToString()), Guid.Parse(item["payment_guid"].ToString())));
            }
            paid_Bills = paid_Bills.Where(balance => balance.AccountId == accountId).
                OrderBy(s => s.Date.Year).
                ToList();


            return paid_Bills;
        }
    }

    public class Period
    {
        public Period(string period)
        {
            Year = int.Parse((period[0].ToString() + period[1].ToString() + period[2].ToString() + period[3].ToString()));
            Month = int.Parse((period[4].ToString() + period[5].ToString()));
        }

        public int Year { get; set; }
        public int Month { get; set; }
    }

   
}