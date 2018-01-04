using AutoMapper;
using BuyWebPage.Data;
using BuyWebPage.Data.Entities;
using BuyWebPage.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyWebPage.Controllers.Api
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        // GET: api/values
        private IBuyRepository Repository;
        private ILogger<OrderController> _Loger;

        public IMapper _mapper;
        private readonly UserManager<Users> _Manager;

        public OrderController(IBuyRepository repository, ILogger<OrderController> Loger, IMapper Mapper,
            UserManager<Users> _manager)
        {
            Repository = repository;
            _Loger = Loger;
            _mapper = Mapper;
            _Manager = _manager;
        }


        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var result = Repository.GetOrdersByUsername(username, includeItems);

                if(result != null)
                { 
                    return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(result));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest($"Error getting the orders {Ex}");

            }
        }
       [HttpGet("{id:int}")]
        public IActionResult Get(int id, bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;
                _Loger.LogInformation("el usuario es " + username);
                var result = Repository.GetOrdersById(username, includeItems, id);

                if(result != null)
                {
                    var viewmodel = _mapper.Map<Order, OrderViewModel>(result);
                    return Ok(viewmodel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest($"Error getting the orders {Ex}");

            }
        }
        //FromBody exige que los campos del objeto se deba extraer del Json o Xml y no del URI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel Model)
        {
            try
            {
                if (ModelState.IsValid) {

                    var usuario = await _Manager.FindByNameAsync(User.Identity.Name);

                    //var item = _mapper.Map<ICollection<OrderItemViewModel>, ICollection<OrderItem>>(Model.Items);

                    var order = _mapper.Map<OrderViewModel, Order>(Model);
                    //order.Items = item;

                    if(order.OrderDate == DateTime.MinValue)
                    {
                        order.OrderDate = DateTime.UtcNow;
                      
                    }
                    order.Usuario = usuario;
                    Repository.AddOrder(order);

                    if (Repository.SaveChanges())
                    {
                        return Created($"here is the new order {Model.Id} ",_mapper.Map<Order, OrderViewModel>(order));
                    }
                }
                else
                {
                    return BadRequest("invalid entries");
                }

            }
            catch (Exception Ex)
            {
                return BadRequest("failed to save the order "+Ex);
            }
            return BadRequest("failed saving the new order ");
        
        }

    }
}
