using System;
using System.Threading.Tasks;
using Xunit;

namespace FrontTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task OnGetAsync_PopulatesThePageModel_WithAListOfMessages()
        {
            //// Arrange
            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase("InMemoryDb");

            //var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
            //var expectedMessages = AppDbContext.GetSeedingMessages();
            //mockAppDbContext.Setup(
            //    db => db.GetMessagesAsync()).Returns(Task.FromResult(expectedMessages));
            //var pageModel = new IndexModel(mockAppDbContext.Object);
    
            //// Act
            //await pageModel.OnGetAsync();

            //// Assert
            //var actualMessages = Assert.IsAssignableFrom<List<Message>>(pageModel.Messages);
            //Assert.Equal(
            //    expectedMessages.OrderBy(m => m.Id).Select(m => m.Text),
            //    actualMessages.OrderBy(m => m.Id).Select(m => m.Text));
        }
    }
}
