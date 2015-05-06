namespace Finance
{
    public enum TransactionType
    {
        [Description("Débito")]
        Debit,
        [Description("Crédito")]
        Credit,
        [Description("Transferência de Crédito")]
        CreditTransfer,
        [Description("Transferência de Débito")]
        DebitTransfer
    }
}
