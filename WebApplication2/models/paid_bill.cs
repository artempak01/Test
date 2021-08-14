using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.models
{
    public class Paid_bill
    {
        public Paid_bill(int accountId, DateTime date, double sum, Guid payment_guid)
        {
            AccountId = accountId;
            Date = date;
            Sum = sum;
            Payment_guid = payment_guid;
        }

        public int AccountId  { get; set; }
        public DateTime Date { get; set; }
        public double Sum  { get; set; }
        public Guid Payment_guid  { get; set; }
    }
}
