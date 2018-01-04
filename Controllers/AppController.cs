using BuyWebPage.Data;
using Microsoft.AspNetCore.DataProtection.AzureStorage;
using BuyWebPage.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuyWebPage.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BuyWebPage.Data.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyWebPage.Controllers
{
    public class AppController : Controller
    {
        private IEmailService _Email;

        IBuyRepository _repository;

        // GET: /<controller>/

        public AppController(IEmailService Email, IBuyRepository Repository)
        {
            _Email = Email;
            _repository = Repository;
        }
   
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            //throw new InvalidOperationException("Bad thing happend");
           
            return View();
        }

        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                _Email.SendEmail(model.Email,"dionisjosue@gmail.com",model.Message);
                ViewBag.UserMessage = "Message Sent";
            }
            else
            {
                ModelState.AddModelError("", "the Message Cant no be sent");
            }
            return View();
        }
        public IActionResult About()
        {
           
            return View();
        }
        //[Authorize]
        public IActionResult Shop()
        {
            /*alternative form
             * var result = Context.Products
                        .OrderBy(p => p.Category)
                        .ToList();
            */
            var result = _repository.getProducts();

            return View(result);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult MyOrder()
        {
            var Orders = _repository.GetOrdersByUsername(this.User.Identity.Name, true);
            return View(Orders);
        }
    }
}
