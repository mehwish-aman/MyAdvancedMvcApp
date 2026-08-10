using Microsoft.AspNetCore.Mvc;

public class InventoryItem
{
    public string ItemName {get;set;}
    public string Description {get;set;}
     public decimal Purchase_Value {get;set;}
    public DateTime Purchase_Date {get;set;}
     public decimal Tax {get;set;}
    public string Company {get;set;}
    public string Total {get;set;}
}