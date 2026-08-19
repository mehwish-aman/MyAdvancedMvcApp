using Microsoft.AspNetCore.Mvc;

public class InventoryController : Controller
{
    private readonly AppDbContext _context;

    public InventoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Add()
    {
        var items = _context.InventoryItems.Where(i => !i.IsDeleted).ToList();
        return View(items);
    }

    [HttpPost]
    public IActionResult Save(InventoryItem model)
    {
        if (!ModelState.IsValid)
    {
        return BadRequest(new { message = "Validation failed. Please check field limits." });
    }

    _context.InventoryItems.Add(model);
    _context.SaveChanges();

    return Ok(new
    {
        message = "Item saved successfully to the database!",
        id = model.Id,
        itemName = model.ItemName,
        description = model.Description,
        purchaseValue = model.PurchaseValue,
        purchaseDate = model.PurchaseDate.ToShortDateString(),
        tax = model.Tax,
        total = model.Total,
        company = model.Company
    });
}
    [HttpPost]
    [Route("Inventory/Delete/{id}")]
public IActionResult Delete(int id)
{
    var item = _context.InventoryItems.Find(id);

    if (item == null)
    {
        return NotFound(new { message = "Item not found." });
    }

    item.IsDeleted = true;
    _context.SaveChanges();

    return Ok(new { message = "Item deleted successfully." });
}

 
}