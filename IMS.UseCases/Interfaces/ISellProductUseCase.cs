﻿using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface ISellProductUseCase
    {
        Task ExecuteAsync(string saleOrderNumber, Product product, int quantity, string doneBy);
    }
}