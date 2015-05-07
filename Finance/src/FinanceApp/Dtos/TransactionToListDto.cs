using App.Helpers;
using Finance;

namespace App.Dtos
{
    public class TransactionListItemDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AccountName { get; set; }
        
        public TransactionListItemDto(Transaction transaction)
        {
            Id = transaction.Id;
            Value = transaction.Value;
            Date = transaction.Date.ToBrString();
            Description = transaction.Description;
            CategoryName = transaction.Category.Name;
            AccountName = transaction.Account.Name;
        }
    }
}