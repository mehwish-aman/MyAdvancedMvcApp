using Microsoft.AspNetCore.Mvc;
//Homecontroller child class ban gai hai controller ki
public class HomeController:Controller
{
    public IActionResult Index()
    {
        return View();
    }

}