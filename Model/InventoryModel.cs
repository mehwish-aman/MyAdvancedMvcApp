using System.ComponentModel.DataAnnotations;

public class InventoryItem
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string ItemName { get; set; }

    [MaxLength(250)]
    public string Description { get; set; }

    [Range(0, 999999)]
    public decimal PurchaseValue { get; set; } =0;

    public DateTime PurchaseDate { get; set; }

    [Range(0, 999999)]
    public decimal Tax { get; set; } = 0;

    [MaxLength(100)]
    public string Company { get; set; }

    [Range(0, 999999)]
    public decimal Total { get; set; }=0;
    [Range(0, 999999)]
     public bool IsDeleted { get; set; } = false;
}