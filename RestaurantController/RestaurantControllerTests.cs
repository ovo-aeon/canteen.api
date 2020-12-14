using System;
using Xunit;
using Moq;
using Canteen.API.Controllers.v1;
using Canteen.Core.Managers;
using Canteen.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantCtr
{
    public class RestaurantControllerTests
    {
        private readonly RestaurantController _sut;
        private readonly Mock<IRestaurantManager> _restaurantMock = new Mock<IRestaurantManager>();
        public RestaurantControllerTests()
        {
            _sut = new RestaurantController(_restaurantMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkHTTPResponse()
        {
            // Arrange
            var listItem = new Restaurant
            {
                Name = "Ovo Joy",
                LogoName = "img.png",
                Long = "56.55"
            };
            _restaurantMock.Setup(r => r.ReadAllRestaurants()).Returns(() => new List<Restaurant> { listItem });

            // Act 
            IActionResult result = _sut.Get();
            var okresult = result as OkObjectResult;

            // Assert

            Assert.NotNull(okresult);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact]
        public void Get_ShouldReturnBadRequestHTTPResponse()
        {
            // Arrange
            var listItem = new Restaurant
            {
                Name = "Ovo Joy",
                LogoName = "img.png",
                Long = "56.55"
            };
            _restaurantMock.Setup(r => r.ReadAllRestaurants()).Returns(() => new List<Restaurant> { });

            // Act 
            IActionResult result = _sut.Get();
            var badresult = result as BadRequestObjectResult;

            // Assert

            Assert.NotNull(badresult);
            Assert.Equal(400, badresult.StatusCode);
        }
    }
}
