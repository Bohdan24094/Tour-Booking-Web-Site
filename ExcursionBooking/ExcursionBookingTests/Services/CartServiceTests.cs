using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcursionBooking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcursionBooking.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using ExcursionBooking.Models;

namespace ExcursionBooking.Services.Tests
{
    [TestClass()]
    public class CartServiceTests
    {
        private CartService CreateCartService(ApplicationDbContext context)
        {
            var loggerMock = new Mock<ILogger<CartService>>();
            return new CartService(context, loggerMock.Object);
        }
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }
        

        [TestMethod()]
        public async Task AddToCartAsync_ShouldAddToCart()
        {
            //Arrange
            var context = CreateInMemoryDbContext();
            var cartService = CreateCartService(context);
            string userId = "SRLH9E-WW48FJ";
            await cartService.AddToCartAsync(1, 2, userId);
            context.SaveChanges();

            //Act
            var result = context.CartItems
                            .Where(c=>c.UserId == userId)
                            .FirstOrDefault();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
        }

        [TestMethod()]
        public async Task GetCartItemsAsync_ShouldReturnList()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var cartService = CreateCartService(context);
            string userId = "SRLH9E-WW48FJ";

            var tour1 = new Tour { Id = 1, Title = "Tour 1", Description = "Description 1", ImageUrl = "ImageUrl1" };
            var tour2 = new Tour { Id = 4, Title = "Tour 4", Description = "Description 4", ImageUrl = "ImageUrl4" };

            context.Tours.Add(tour1);
            context.Tours.Add(tour2);
            context.SaveChanges();

            var cartItem1 = new CartItem { UserId = userId, Quantity = 2, TourId = 1 };
            var cartItem2 = new CartItem { UserId = userId, Quantity = 3, TourId = 4 };

            context.CartItems.Add(cartItem1);
            context.CartItems.Add(cartItem2);
            context.SaveChanges();

            // Act
            var result = await cartService.GetCartItemsAsync(userId);

            // Assert
            Assert.IsNotNull(result);

            // Additional checks to verify correct items are returned
            var item1 = result.FirstOrDefault(c => c.TourId == 1);
            var item2 = result.FirstOrDefault(c => c.TourId == 4);

            Assert.IsNotNull(item1);
            Assert.AreEqual(2, item1.Quantity);

            Assert.IsNotNull(item2);
            Assert.AreEqual(3, item2.Quantity);
        }
    }
}