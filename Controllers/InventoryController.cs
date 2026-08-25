using Microsoft.AspNetCore.Mvc; //enabling mvc services

public class InventoryController : Controller //inheriting render, req features etc
{ 

                                            //Constructor depandency injection
     //Ye poora code sirf ek "database connection ka setup" hai, taake har method ke andar _context.InventoryItems...
     //likh ke hum database se data le/de sakein, begair baar baar naya connection banaye.

    private readonly AppDbContext _context;   //Ye sirf keh raha hai: "Is Controller ke paas ek _context naam ki cheez hogi, 
    //jo database se baat karegi." Abhi khali hai, sirf declare kiya.

    public InventoryController(AppDbContext context)  //Jab bhi website kholtengy ho (Inventory page pe jaty hain), 
    //ASP.NET khud ek AppDbContext bana kar is Controller ko de deta hai khud kuch karna nahi padta, ye automatic hai.
    {
        _context = context;  //Jo cheez ASP.NET ne di, use humne apne _context field mein save kar liya, taake niche jitne 
        //bhi methods hain (Add, Save, Delete), sab isko use kar sakein database se baat karne ke liye.
    }


                                                    //Add method
     //  will handle the GET request to display the inventory items page.
     //  It will fetch the inventory items from the database and pass them to the view.
     [HttpGet]
    public IActionResult Add()
    {    //fetching all the inventory items from the database where IsDeleted is false and passing them to the view
        //InventoryItems table se sirf non-deleted rows fetch karke List banai jati hai (EF Core khud SQL query generate karta hai), 
        //phir wo List Add.cshtml ko Model ke tor pe bhej di jati hai taake @foreach se table mein dikhai ja sake.
        var items = _context.InventoryItems.Where(i => !i.IsDeleted).OrderByDescending(i=>i.Id).ToList();
        return View(items);
    }

                                                    //Save
//Save method will handle the POST request to save the inventory item to the database
//Browser sy fetch req ae having formdata. asp.net us data ka inventoryitem model  ka obj bana dega, name ko name mein, 
//tax ko tex mein. 
//ye sb  method chalny sy pehly hi ho jata hai.
        [HttpPost]
    public IActionResult Save(InventoryItem model)
    {    //validation, checks if data has any invalid value like not in range etc then ye error msg shw kryga

        //if not valid
        if (!ModelState.IsValid)
    {   //badRequest http ka 400 status code ky sath error bhjeta hai
        return BadRequest(new { message = "Validation failed. Please check field limits." });
    }
        //if valid  model binding yahain hogi, aik obj bana dega r usmwin from data dal dega. 
    _context.InventoryItems.Add(model);  //mark to save in db 
    _context.SaveChanges();   //is py EF core insert query banata hai khud r khud ba khud id mein data save ho jata hai.
         //responce 
         //new- http- 200 okay status code. aik anonymous obj mein srf data hota hai auto convert json mein ho kr phir 
         //S ko milta hai new item ki sari details. JS us data ko table mein show krta hai.
    return Ok(new
    {
        message = "Item saved successfully to the database!",
        id = model.Id,
        itemName = model.ItemName,
        description = model.Description,
        purchaseValue = model.PurchaseValue,
        purchaseDate = model.PurchaseDate.ToShortDateString(),
        rawDate = model.PurchaseDate.ToString("yyyy-MM-dd"),
        tax = model.Tax,
        total = model.Total,
        company = model.Company
    });
}   
                                                    //Update Method
//Update method mein Id URL se aati hai (/Inventory/Update/7 → id=7), aur baaki form data request body se aata hai 
// dono alag jagah se, ek hi method mein milte hain. Find(id) se purani row database se nikal ke uski properties naye 
// data se manually replace ki jati hain (Id/IsDeleted chhod ke). SaveChanges() call hote hi EF Core khud samajh jata hai 
// ye ek UPDATE hai (na ke naya INSERT), kyunki row already "tracked" thi.

 [HttpPost]
[Route("Inventory/Update/{id}")]
public IActionResult Update(int id, InventoryItem model)
{
    var existingItem = _context.InventoryItems.Find(id);

    if (existingItem == null)
    {
        return NotFound(new { message = "Item not found." });
    }

    existingItem.ItemName = model.ItemName;
    existingItem.Description = model.Description;
    existingItem.PurchaseValue = model.PurchaseValue;
    existingItem.PurchaseDate = model.PurchaseDate;
    existingItem.Tax = model.Tax;
    existingItem.Total = model.Total;
    existingItem.Company = model.Company;

    _context.SaveChanges();

    return Ok(new
    {
        message = "Item updated successfully!",
        id = existingItem.Id,
        itemName = existingItem.ItemName,
        description = existingItem.Description,
        purchaseValue = existingItem.PurchaseValue,
        purchaseDate = existingItem.PurchaseDate.ToShortDateString(),
        tax = existingItem.Tax,
         rawDate = existingItem.PurchaseDate.ToString("yyyy-MM-dd"),
        total = existingItem.Total,
        company = existingItem.Company
    });
}

                                                /// Delete Method
    //URL se id leke Find(id) se row dhoondhi jati hai,
    //  IsDeleted = true set kiya jata hai (row delete nahi hoti, bas flag change hota hai), 
    // SaveChanges() se UPDATE query chalti hai, aur response mein sirf success message bheja jata hai 
    // kyunki row hatana JavaScript khud DOM se kar leti hai.   

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
    _context.SaveChanges(); //update query chalegi

    return Ok(new { message = "Item deleted successfully." });
}

 
}