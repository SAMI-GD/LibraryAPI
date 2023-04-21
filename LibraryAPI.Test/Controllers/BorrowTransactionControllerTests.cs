using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Controllers;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Test.Controllers
{
    public class BorrowTransactionControllerTests 
    {
        private readonly BorrowTransactionsController _controller;
        private readonly IBorrowTransactionRepository _borrowTransactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly IMapper _mapper;

        public BorrowTransactionControllerTests()
        {
            _borrowTransactionRepository = A.Fake<IBorrowTransactionRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _libraryItemRepository = A.Fake<ILibraryItemRepository>();
            _mapper = A.Fake<IMapper>();

            _controller = new BorrowTransactionsController(_borrowTransactionRepository, _userRepository, _libraryItemRepository, _mapper);
        }

        [Fact]
        public async Task AssignBookAsync_ShouldReturnCreatedResponse_WhenSuccessful()
        {
            // Arrange
            var assignBookDTO = new AssignBookDTO { UserID = 1, ItemID = 2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };
            var user = new User { UserID = 1, FirstName = "Samila" };
            var libraryItem = new LibraryItem { ItemID = 2, Title = "The Great War", AvailabilityStatus = AvailabilityStatus.Available };

            A.CallTo(() => _userRepository.GetByIdAsync(1)).Returns(user);
            A.CallTo(() => _libraryItemRepository.GetByIdAsync(2)).Returns(libraryItem);

            // Act
            var result = await _controller.AssignBookAsync(assignBookDTO);

            // Assert
            result.Result.Should().BeOfType<CreatedAtRouteResult>();
            var createdAtRouteResult = result.Result as CreatedAtRouteResult;
            createdAtRouteResult.RouteName.Should().Be("GetTransactionById");
            createdAtRouteResult.RouteValues["id"].Should().BeOfType<int>();
        }
        [Fact]
        public async Task AssignBookAsync_ShouldReturnNotFound_WhenUserNotFound()
        {
            // Arrange
            var assignBookDTO = new AssignBookDTO { UserID = 1, ItemID = 2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };

            A.CallTo(() => _userRepository.GetByIdAsync(1)).Returns((User)null);

            // Act
            var result = await _controller.AssignBookAsync(assignBookDTO);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AssignBookAsync_ShouldReturnNotFound_WhenLibraryItemNotFound()
        {
            // Arrange
            var assignBookDTO = new AssignBookDTO { UserID = 1, ItemID = 2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };
            var user = new User { UserID = 1, FirstName = "Samila" };

            A.CallTo(() => _userRepository.GetByIdAsync(1)).Returns(user);
            A.CallTo(() => _libraryItemRepository.GetByIdAsync(2)).Returns((LibraryItem)null);

            // Act
            var result = await _controller.AssignBookAsync(assignBookDTO);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AssignBookAsync_ShouldReturnBadRequest_WhenLibraryItemNotAvailable()
        {
            // Arrange
            var assignBookDTO = new AssignBookDTO { UserID = 1, ItemID = 2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };
            var user = new User {UserID = 1, FirstName = "Samila" };
            var libraryItem = new LibraryItem { ItemID = 2, Title = "The Great War", AvailabilityStatus = AvailabilityStatus.Borrowed };

            A.CallTo(() => _userRepository.GetByIdAsync(1)).Returns(user);
            A.CallTo(() => _libraryItemRepository.GetByIdAsync(2)).Returns(libraryItem);

            // Act
            var result = await _controller.AssignBookAsync(assignBookDTO);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task GetUserBorrowingHistory_ShouldReturnUserBorrowTransactions()
        {
            // Arrange
            int userId = 1;
            var borrowTransactions = new List<BorrowTransaction>
    {
        new BorrowTransaction { TransactionID = 1, UserID = userId, ItemID = 1 },
        new BorrowTransaction { TransactionID = 2, UserID = userId, ItemID = 2 }
    };
            var borrowTransactionDTOs = new List<BorrowTransactionDTO>
    {
        new BorrowTransactionDTO { TransactionID = 1, UserID = userId, ItemID = 1 },
        new BorrowTransactionDTO { TransactionID = 2, UserID = userId, ItemID = 2 }
    };

            A.CallTo(() => _borrowTransactionRepository.GetBorrowingHistoryByUserIdAsync(userId))
                .Returns(borrowTransactions);

            // Set up the fake IMapper behavior
            A.CallTo(() => _mapper.Map<IEnumerable<BorrowTransactionDTO>>(borrowTransactions))
                .Returns(borrowTransactionDTOs);

            // Act
            var result = await _controller.GetUserBorrowingHistory(userId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedBorrowTransactions = okResult.Value.Should().BeAssignableTo<IEnumerable<BorrowTransactionDTO>>().Subject;
            returnedBorrowTransactions.Should().HaveCount(2);
        }

    }
}
