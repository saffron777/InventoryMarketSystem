﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBussiness
{
    public class InventoryTransaction
    {
        public int InventoryTransactionId { get; set; }

        [Required]
        public InventoryTransactionType ActivityType { get; set; }

        [Required]
        public int InventoryId { get; set; }

        [Required]
        public int QuantityBefore { get; set; }

        [Required]
        public int QuantityAfter { get; set; }
        public string? PONumber { get; set; }
        public string? ProductionNumber { get; set; }

        public double? UnitPrice { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string DoneBy { get; set; }


        public Inventory Inventory { get; set; }


    }
}
