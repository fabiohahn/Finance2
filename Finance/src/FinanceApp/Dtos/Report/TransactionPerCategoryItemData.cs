using System.Collections.Generic;
using System.Linq;

namespace App.Dtos.Report
{
    public class TransactionPerCategoryItemData
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public IList<TransactionValue> Values { get; set; }
        public decimal Average { get { return Values.Average(x => x.Value); } }
        public decimal Sum { get { return Values.Sum(x => x.Value); } }

        public TransactionPerCategoryItemData(string category, int categoryId)
        {
            Category = category;
            CategoryId = categoryId;
            Values = new List<TransactionValue>();
        }

        public void AddValue(TransactionValue transactionValue)
        {
            Values.Add(transactionValue);
        }
    }
}