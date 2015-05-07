using System;
using System.Collections.Generic;

namespace App.Dtos.Report
{
    public class BallancePerAccountData
    {
        public IList<AccountData> AccountBalance { get; set; }
        public DateTime DateLimit { get; set; }

        public BallancePerAccountData()
        {
            AccountBalance = new List<AccountData>();
        }
        public void AddData(string accountName, decimal ballance)
        {
            AccountBalance.Add(new AccountData(accountName, ballance));
        }
    }

    public class AccountData
    {
        public string AccountName { get; set; }
        public decimal Ballance { get; set; }

        public AccountData(string accountName, decimal ballance)
        {
            AccountName = accountName;
            Ballance = ballance;
        }
    }
}