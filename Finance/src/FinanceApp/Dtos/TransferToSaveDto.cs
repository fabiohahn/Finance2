using System;
using System.Collections.Generic;
using App.Helpers;
using Finance;

namespace App.Dtos
{
    public class TransferToSaveDto
    {
        public decimal Value { get; set; }
        public string Date { get; set; }
        public int OriginAccountId { get; set; }
        public int DestinyAccountId { get; set; }
        public string Description { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }

        public TransferToSaveDto(){}

        public TransferToSaveDto(IEnumerable<Account> accounts)
        {
            TransformAccounts(accounts);
            Date = DateTime.Today.ToUsString();
        }

        public TransferToSaveDto(decimal value, DateTime date, int originAccountId, int destinyAccountId)
        {
            Value = value;
            Date = date.ToBrString();
            OriginAccountId = originAccountId;
            DestinyAccountId = destinyAccountId;
        }

        private void TransformAccounts(IEnumerable<Account> accounts)
        {
            Accounts = new List<AccountDto>();

            foreach (var account in accounts)
            {
                Accounts.Add(new AccountDto(account));
            }
        }
    }
}