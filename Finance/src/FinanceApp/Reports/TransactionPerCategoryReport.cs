using System;
using System.Collections.Generic;
using System.Linq;
using App.Dtos.Report;
using Finance;
using Finance.IRepositories;

namespace App.Reports
{
    public class TransactionPerCategoryReport
    {
        private IList<Transaction> _transactions;
        private TransactionPerCategoryReportDto _report;
        private IEnumerable<Category> _categories;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionPerCategoryReport(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public TransactionPerCategoryReportDto GetReport(int propertyId, DateTime initialDate, DateTime finalDate)
        {
            _transactions = _transactionRepository.GetAllThatAreNotTransfer(propertyId, initialDate, finalDate);
            _categories = GetCategories();

            _report = new TransactionPerCategoryReportDto(initialDate, finalDate, _categories);

            foreach (var category in _categories)
            {
                AddCategoryToReport(category);
            }
            return _report;
        }

        private void AddCategoryToReport(Category category)
        {
            var categoryData = CreateTransactionPerCategoryData(category);

            if (category.TransactionType == TransactionType.Credit)
                _report.AddCredit(categoryData);
            else
                _report.AddDebit(categoryData);
        }

        private TransactionPerCategoryItemData CreateTransactionPerCategoryData(Category category)
        {
            var categoryData = new TransactionPerCategoryItemData(category.Name, category.Id);

            for (var date = _report.InitialDate; date <= _report.FinalDate; date = date.AddMonths(1))
            {
                var sumOfTheMonth = SumTransactionsForCategoryAndDate(category.Name, date);
                categoryData.AddValue(sumOfTheMonth);
                AddToSumOfTheMonth(sumOfTheMonth.Copy(), category.TransactionType == TransactionType.Credit);
            }
            return categoryData;
        }

        private void AddToSumOfTheMonth(TransactionValue sumOfTheMonth, bool credit)
        {
            if (credit)
                _report.AddCreditForMonth(sumOfTheMonth);
            else
                _report.AddDebitsForMonth(sumOfTheMonth);
        }

        private IEnumerable<Category> GetCategories()
        {
            return _transactions.GroupBy(x => x.Category.Name).Select(y => y.First().Category).ToList().OrderByDescending(x => x.TransactionType).ThenBy(x => x.Name).ToList();
        }

        private TransactionValue SumTransactionsForCategoryAndDate(string category, DateTime date)
        {
            var sumOfThisCategory =
                _transactions.Where(
                    x => x.Category.Name == category && x.Date.Month == date.Month && x.Date.Year == date.Year)
                    .Sum(y => y.Value);

            return new TransactionValue(sumOfThisCategory, date);
        }
    }
}