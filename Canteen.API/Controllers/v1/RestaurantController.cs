using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canteen.Core.BusinessModels;
using Canteen.Core.Entities;
using Canteen.Core.Managers;
using Canteen.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using static Canteen.API.Middlewares.Authentication;

namespace Canteen.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantManager _restaurantManager;

        public RestaurantController(IRestaurantManager restaurantManager)
        {
            _restaurantManager = restaurantManager;
        }
        // GET: api/Restaurant
        [HttpGet]
        public IActionResult Get()
        {
            var res = _restaurantManager.ReadAllRestaurants().ToList();
            if (res.Count > 0) return Ok(new APIResponse { IsSuccess = true, Message = $"Retrieved Restaurants{res.Count}", Response = res });
            return BadRequest(new APIResponse { Message = $"Retrieved Restaurants{res.Count}", Response = res });
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Restaurant
        [HttpPost]
        [Route("create")]
        [AuthorizeRoles(Roles.Vendor, Roles.Admin)]
        public async Task<IActionResult> Post([FromForm] RestaurantModel model)
        {
            var createdRest = await _restaurantManager.CreateRestaurant(model);
            if (createdRest != null) return Ok(new APIResponse { IsSuccess = true, Message = $"Created Restaurant{createdRest.Name}", Response = createdRest });
            return BadRequest(new APIResponse { Message = $"Couldn't create restaurant {model.Name}", Response = createdRest });
        }

        // PUT: api/Restaurant/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
