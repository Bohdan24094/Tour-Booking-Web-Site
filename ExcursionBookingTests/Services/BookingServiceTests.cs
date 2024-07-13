using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExcursionBooking.Services.Tests
{
    [TestClass]
    public class BookingServiceTests
    {
        private BookingService CreateBookingService(ApplicationDbContext context)
        {
            var loggerMock = new Mock<ILogger<BookingService>>();
            var cartServiceMock = new Mock<CartService>(context, new Mock<ILogger<CartService>>().Object);
            return new BookingService(context, cartServiceMock.Object, loggerMock.Object);
        }

        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [TestMethod]
        public async Task GetTourAsync_ShouldReturnTour_WhenTourExists()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var tour = new Tour { Id = 1, Title = "Test Tour", Description = "Test Description", ImageUrl = "test.jpg" };
            context.Tours.Add(tour);
            context.SaveChanges();

            var bookingService = CreateBookingService(context);

            // Act
            var result = await bookingService.GetTourAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Tour", result.Title);
        }

        [TestMethod]
        public async Task ProcessStep2Async_ShouldReturnFalse_WhenTourDoesNotExist()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var bookingService = CreateBookingService(context);

            // Act
            var result = await bookingService.ProcessStep2Async(1, 2, 2);

            // Assert
            Assert.IsFalse(result.success);
        }

        [TestMethod]
        public async Task ProcessStep2Async_ShouldReturnFalse_WhenPersonsNotSpecified()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var tour = new Tour { Id = 1, Title = "Test Tour", Description = "Test Description", ImageUrl = "test.jpg" };
            context.Tours.Add(tour);
            context.SaveChanges();

            var bookingService = CreateBookingService(context);

            // Act
            var result = await bookingService.ProcessStep2Async(1, 0, 0);

            // Assert
            Assert.IsFalse(result.success);
            Assert.AreEqual(0, result.adults);
            Assert.AreEqual(0, result.children);
        }
    }
}