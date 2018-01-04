using Microsoft.Extensions.Logging;
using BuyWebPage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuyWebPage.Data.Entities;
using AutoMapper;
using BuyWebPage.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyWebPage.Controllers.Api
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        public IBuyRepository Context;
        public ILogger<ProductController> _Loger { get; }
        public IMapper Map;

        public ProductController(IBuyRepository context, 
            ILogger<ProductController>Loger, IMapper map)
        {
            Context = context;
            _Loger = Loger;
            Map = map;
        }

       
        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = Context.getProducts();

                return Ok(result);

            }
            catch(Exception Ex)
            {
                return BadRequest($"An error occur getting the products {Ex}");
            }
        }
        
    }
}
