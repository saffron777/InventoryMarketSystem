﻿using IMS.CoreBussiness;

namespace IMS.UseCases.Reports
{
    public interface ISearchProductTransactionsUseCase
    {
        Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactiontype);
    }
}