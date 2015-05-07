using System;

namespace App.Dtos.Report
{
    public class TransactionValue
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public TransactionValue(decimal value, DateTime date)
        {
            Value = value;
            Date = date;
        }

        public TransactionValue Copy()
        {
            return new TransactionValue(Value,Date);
        }
    }
}