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
    [HttpPost]
//database nhi hai abhi is liye srf conformation ayegi abhi 
//r hm model ka data hai usy hm confirm view ko b bhj rhy hain taky 
//wahan b use ho sakey 
    public IActionResult Save(InventoryItem model)
    {
        return View("confirm",model);
    }
}