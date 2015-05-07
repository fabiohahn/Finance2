using System;
using System.Collections.Generic;
using System.Linq;
using Finance;

namespace App.Dtos.Report
{
    public class TransactionPerCategoryReportDto
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }


        public IList<CategoryDto> Categories;
        public IList<TransactionPerCategoryItemData> Credits;
        public IList<TransactionPerCategoryItemData> Debits;
        public TransactionPerCategoryItemData CreditsSum;
        public TransactionPerCategoryItemData DebitsSum;
        public TransactionPerCategoryItemData Balance;

        public TransactionPerCategoryReportDto(DateTime initialDate, DateTime finalDate, IEnumerable<Category> categories)
        {
            InitialDate = initialDate;
            FinalDate = finalDate;
            FillCategories(categories);
            Credits = new List<TransactionPerCategoryItemData>();
            Debits = new List<TransactionPerCategoryItemData>();
            CreditsSum = new TransactionPerCategoryItemData("Créditos", 0);
            DebitsSum = new TransactionPerCategoryItemData("Débitos", 0);
            Balance = new TransactionPerCategoryItemData("Saldo", 0);
        }

        protected void FillCategories(IEnumerable<Category> categories)
        {
            Categories = new List<CategoryDto>();

            foreach (var category in categories)
            {
                Categories.Add(new CategoryDto(category));
            }
        }

        public void AddCredit(TransactionPerCategoryItemData item)
        {
            Credits.Add(item);
        }

        public void AddDebit(TransactionPerCategoryItemData item)
        {
            Debits.Add(item);
        }

        public void AddCreditForMonth(TransactionValue sumOfTheMonth)
        {
            var creditsOfMonth = CreditsSum.Values.FirstOrDefault(x => x.Date.Month == sumOfTheMonth.Date.Month);
            if (creditsOfMonth != null)
                creditsOfMonth.Value += sumOfTheMonth.Value;
            else
                CreditsSum.Values.Add(sumOfTheMonth);

            AddBalanceForMonth(sumOfTheMonth, true);
        }

        public void AddDebitsForMonth(TransactionValue sumOfTheMonth)
        {
            var debitsOfMonth = DebitsSum.Values.FirstOrDefault(x => x.Date.Month == sumOfTheMonth.Date.Month);
            if (debitsOfMonth != null)
                debitsOfMonth.Value += sumOfTheMonth.Value;
            else
                DebitsSum.Values.Add(sumOfTheMonth);

            AddBalanceForMonth(sumOfTheMonth, false);
        }

        public void AddBalanceForMonth(TransactionValue sumOfTheMonth, bool credit)
        {
            var value = sumOfTheMonth.Value;
            if (!credit)
                value *= -1;

            var balanceOfMonth = Balance.Values.FirstOrDefault(x => x.Date.Month == sumOfTheMonth.Date.Month);
            if (balanceOfMonth != null)
                balanceOfMonth.Value += value;
            else
                Balance.Values.Add(new TransactionValue(value, sumOfTheMonth.Date));
        }
    }
}