using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuyWebPage.Data.Entities;
using BuyWebPage.Data;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BuyWebPage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyWebPage.Controllers.Api
{
    [Route("api/order/{orderid}/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemController : Controller
    {

        public IBuyRepository _Contex;
        public ILogger<OrderItemController> Logger;
        public IMapper Map;

        public OrderItemController(IBuyRepository _contex,
            ILogger<OrderItemController> Logger, IMapper map)
        {
            _Contex = _contex;
            this.Logger = Logger;
            Map = map;
        }



        // GET: api/values
        [HttpGet]
        public IActionResult Get(int orderid)
        {
            try
            {
                var Username = User.Identity.Name;
                var order = _Contex.GetOrdersById(Username,true, orderid);

                if (order != null)
                {
                    return Ok(Map.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
                }
                return NotFound();


            }
            catch (Exception ex)
            {
                return BadRequest("could not find items for that order" + ex);
            }
        }
        [HttpGet("{ItemId:int}")]
        public IActionResult Get(int orderid, int ItemID)
        {
            try
            {
                var Username = User.Identity.Name;
                var order = _Contex.GetOrdersById(Username,true, orderid);

                if (order != null)
                {
                    var item = order.Items.Where(i => i.OrderItemId == ItemID).FirstOrDefault();
                    if (item != null)
                    {
                        return Ok(Map.Map<OrderItem, OrderItemViewModel>(item));
                    }
                    return NotFound();
                }
                return NotFound();


            }
            catch (Exception ex)
            {
                return BadRequest("could not find the item for that order" + ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderItemViewModel ModelItem, int orderid)
        {

            try
            {
                if (ModelState.IsValid )
                {
                    var Username = User.Identity.Name;
                    var orderitem = Map.Map<OrderItemViewModel, OrderItem>(ModelItem);
                    _Contex.AddOrderItem(Username,orderid, orderitem);

                    if (_Contex.SaveChanges())
                    {
                       return Created($"new item created for the {orderid}",
                            Map.Map<OrderItem, OrderItemViewModel>(orderitem));
                    }
                    return BadRequest("failed to save the new item");
                }
                else
                {
                    return BadRequest("invalid entries");

                }
            }
            catch (Exception ex)
            {
                return BadRequest("could not save the item for that order" + ex);
            }
            

        }

    }

}
