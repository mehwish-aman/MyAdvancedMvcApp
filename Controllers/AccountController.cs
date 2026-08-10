using Microsoft.AspNetCore.Mvc;
public class AccountController : Controller
{
    //when user will open app or login page
    [HttpGet]
        public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        //checked for values login
        if(model.Username=="admin"&& model.Password=="1234")
        {
            return RedirectToAction("Add","Inventory");
        }
        else
        ViewBag.error="Invalid Username or password";
        return View();
    }
}
