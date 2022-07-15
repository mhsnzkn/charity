using Business.Concrete;
using Data.Entities;
using DataAccess.Abstract;
using Moq;

namespace Test
{
    public class AgreementManagerTests
    {
        public Mock<IAgreementDal> mock = new();
        [Fact]
        public async Task GetById_WhenCalledValidId_ThenReturnEntity()
        {
            /// Assign
            mock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(new Agreement { Id = 1, Title = "Test Title", Content = "", Order = 1});
            AgreementManager manager = new (mock.Object, null, null, null);

            /// Act
            var result = await manager.GetById(1);

            /// Assert
            Assert.Equal(1, result.Id);

        }
        [Fact]
        public async Task GetById_WhenCalledInvalidId_ThenReturnNull()
        {
            /// Assign
            mock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(new Agreement { Id = 1, Title = "Test Title", Content = "", Order = 1});
            AgreementManager manager = new (mock.Object, null, null, null);

            /// Act
            var result = await manager.GetById(2);

            /// Assert
            Assert.Null(result);

        }
        [Fact]
        public async Task GetById_WhenCalledInvalidId_ThenReturnNull2()
        {
            /// Assign
            mock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(new Agreement { Id = 1, Title = "Test Title", Content = "", Order = 1});
            AgreementManager manager = new (mock.Object, null, null, null);

            /// Act
            var result = await manager.GetById(2);

            /// Assert
            Assert.Null(result);

        }
    }
}