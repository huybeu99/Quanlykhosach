using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.Models;
using System.Diagnostics;

namespace QuanLyKhoSach.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           
            return File("~/Book.html", "text/html");
        }
    }
}