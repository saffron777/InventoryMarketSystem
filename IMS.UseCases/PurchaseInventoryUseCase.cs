﻿using IMS.CoreBussiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;

        public PurchaseInventoryUseCase(
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.inventoryRepository = inventoryRepository;
        }
        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string dondeBy)
        {
            await this.inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, inventory.Price, dondeBy);
            inventory.Quantity += quantity;

            await this.inventoryRepository.UpdateInventoryAsync(inventory);

        }
    }
}
