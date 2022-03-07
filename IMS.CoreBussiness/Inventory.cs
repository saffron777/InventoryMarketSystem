using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBussiness
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        [Required(ErrorMessage = "debe ingresar el nombre del inventario")]
        public string? InventoryName { get; set; }

        [Range(0,int.MaxValue, ErrorMessage ="Ingrese una cantidad")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Ingrese un precio")]
        public double Price { get; set; }

        public List<ProductInventory>?  ProductInventories { get; set; }

    }
}