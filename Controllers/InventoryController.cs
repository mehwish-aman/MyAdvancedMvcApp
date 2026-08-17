using Microsoft.AspNetCore.Mvc;
public class InventoryController: Controller
{
    [HttpGet]

    //form dikhanay kay liyey
    public IActionResult Add()
    {
        return View();
    }
    //jab save ho to yeh ho
//database nhi hai abhi is liye srf conformation ayegi abhi 
//r hm model ka data hai usy hm confirm view ko b bhj rhy hain taky 
//wahan b use ho sakey 
   [HttpPost]
   public IActionResult Save(InventoryItem model)
   {
    // Database nahi hai abhi, isliye sirf success signal bhej rahe hain
    // Poora HTML page return nahi kar rahe, kyunki JavaScript ko sirf confirmation chahiye 
    //so js srf yeh message dikhayega
    return Ok(new { message = "Item received successfully (not saved to database yet)" });
   }
    
}