using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using App.Helpers;
using App.Reports;
using App.Dtos.Report;

namespace FinanceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly BalancePerAccount _balancePerAccount;
        private readonly TransactionPerCategoryReport _transactionPerCategoryReport;

        public ReportsController()
        {
            var transactionRepository = RepositoryFactory.GetTransactionRepository();
            _balancePerAccount = new BalancePerAccount(transactionRepository);
            _transactionPerCategoryReport = new TransactionPerCategoryReport(transactionRepository);
        }

        [HttpGet("balancePerAccount")]
        public BallancePerAccountData BalancePerAccount(DateTime? dateLimit)
        {
            var searchedDate = dateLimit.GetTodayIfNull();

            return _balancePerAccount.GetReport(SeletedPropertyId, searchedDate);           
        }

        [HttpGet("transactionPerCategory")]
        public TransactionPerCategoryReportDto TransactionPerCategory(DateTime? initialDate, DateTime? finalDate)
        {
            var initialPeriod = initialDate.GetFirstFromSixMonthAgo();
            var finalPeriod = finalDate.GetTodayIfNull();

            return _transactionPerCategoryReport.GetReport(SeletedPropertyId, initialPeriod, finalPeriod);
        }
    }
}
