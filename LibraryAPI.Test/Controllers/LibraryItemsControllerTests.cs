using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Controllers;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Test.Controllers
{
    public class LibraryItemsControllerTests
    {
        private readonly ILibraryItemRepository _fakeRepository;
        private readonly IBorrowTransactionRepository _fakeBorrowTransactionRepository;
        private readonly IMapper _fakeMapper;

        public LibraryItemsControllerTests()
        {
            _fakeRepository = A.Fake<ILibraryItemRepository>();
            _fakeBorrowTransactionRepository = A.Fake<IBorrowTransactionRepository>();
            _fakeMapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithListOfLibraryItemDTOs()
        {
            // Arrange
            var fakeLibraryItems = new List<LibraryItem>()
    {
        new LibraryItem { ItemID = 1, Title = "Book 1" },
        new LibraryItem { ItemID = 2, Title = "Book 2" },
        new LibraryItem { ItemID = 3, Title = "Book 3" }
    };

            A.CallTo(() => _fakeRepository.GetAllAsync())
                .Returns(fakeLibraryItems);

            var fakeLibraryItemDTOs = new List<LibraryItemDTO>()
    {
        new LibraryItemDTO { ItemID = 1, Title = "Book 1" },
        new LibraryItemDTO { ItemID = 2, Title = "Book 2" },
        new LibraryItemDTO { ItemID = 3, Title = "Book 3" }
    };

            A.CallTo(() => _fakeMapper.Map<IEnumerable<LibraryItemDTO>>(fakeLibraryItems))
                .Returns(fakeLibraryItemDTOs);

            var controller = new LibraryItemsController(_fakeRepository,_fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<LibraryItemDTO>>>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeLibraryItemDTOs);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOkResult_WithLibraryItemDTO()
        {
            // Arrange
            var itemId = 1;
            var fakeLibraryItem = new LibraryItem { ItemID = itemId, Title = "Book 1" };

            A.CallTo(() => _fakeRepository.GetByIdAsync(itemId))
                .Returns(fakeLibraryItem);

            var fakeLibraryItemDTO = new LibraryItemDTO { ItemID = itemId, Title = "Book 1" };

            A.CallTo(() => _fakeMapper.Map<LibraryItemDTO>(fakeLibraryItem))
                .Returns(fakeLibraryItemDTO);

            var controller = new LibraryItemsController(_fakeRepository,_fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var result = await controller.GetByIdAsync(itemId);

            // Assert
            result.Should().BeOfType<ActionResult<LibraryItemDTO>>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeLibraryItemDTO);
        }
        [Fact]
        public async Task SearchByTitleAsync_ReturnsBadRequest_WhenTitleIsNull()
        {
            // Arrange
            string title = null;

            var controller = new LibraryItemsController(_fakeRepository,_fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var result = await controller.SearchByTitleAsync(title);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should().Be("Title must not be empty.");
        }

        [Fact]
        public async Task SearchByTitleAsync_ReturnsNotFound_WhenNoItemsMatchTitle()
        {
            // Arrange
            var title = "non-existent title";

            A.CallTo(() => _fakeRepository.SearchByTitleAsync(title))
                .Returns(Enumerable.Empty<LibraryItem>());

            var controller = new LibraryItemsController(_fakeRepository,_fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var result = await controller.SearchByTitleAsync(title);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            ((NotFoundObjectResult)result).Value.Should().Be("No items found with the given title.");
        }
        [Fact]
        public async Task SearchByTitleAsync_ReturnsOkResult_WithListOfLibraryItemBasicDTOs()
        {
            // Arrange
            var title = "Book";
            var fakeLibraryItems = new List<LibraryItem>
    {
        new LibraryItem { ItemID = 1, Title = "Book 1", AvailabilityStatus = AvailabilityStatus.Available },
        new LibraryItem { ItemID = 2, Title = "Book 2", AvailabilityStatus = AvailabilityStatus.Borrowed },
        new LibraryItem { ItemID = 3, Title = "Book 3", AvailabilityStatus = AvailabilityStatus.Reserved },
    };

            A.CallTo(() => _fakeRepository.SearchByTitleAsync(title))
                .Returns(fakeLibraryItems);

            var fakeLibraryItemBasicDTOs = new List<LibraryItemBasicDTO>
    {
        new LibraryItemBasicDTO {  Title = "Book 1", AvailabilityStatus = AvailabilityStatus.Available },
        new LibraryItemBasicDTO {  Title = "Book 2", AvailabilityStatus = AvailabilityStatus.Borrowed },
        new LibraryItemBasicDTO {  Title = "Book 3", AvailabilityStatus = AvailabilityStatus.Reserved },
    };

            A.CallTo(() => _fakeMapper.Map<IEnumerable<LibraryItemBasicDTO>>(fakeLibraryItems))
                .Returns(fakeLibraryItemBasicDTOs);

            var controller = new LibraryItemsController(_fakeRepository,_fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var result = await controller.SearchByTitleAsync(title);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(fakeLibraryItemBasicDTOs);
        }

        [Fact]
        public async Task SearchByAuthorAndAvailabilityAsync_ReturnsOkResult_WithListOfLibraryItemBasicDTOs()
        {
            // Arrange
            var author = "John Williams";
            var availabilityStatus = AvailabilityStatus.Available;

            var fakeLibraryItems = new List<LibraryItem>
    {
        new LibraryItem { ItemID = 1, Title = "Book 1", Author = author, AvailabilityStatus = availabilityStatus },
        new LibraryItem { ItemID = 2, Title = "Book 2", Author = "Jane Smith", AvailabilityStatus = availabilityStatus }
    };

            A.CallTo(() => _fakeRepository.SearchByAuthorAndAvailabilityAsync(author, availabilityStatus))
                .Returns(fakeLibraryItems);

            var fakeLibraryItemBasicDTOs = new List<LibraryItemBasicDTO>
    {
        new LibraryItemBasicDTO { Title = "Book 1", Author = author, AvailabilityStatus = availabilityStatus }
    };

            A.CallTo(() => _fakeMapper.Map<IEnumerable<LibraryItemBasicDTO>>(fakeLibraryItems))
                .Returns(fakeLibraryItemBasicDTOs);

            var controller = new LibraryItemsController(_fakeRepository, _fakeBorrowTransactionRepository, _fakeMapper);

            // Act
            var actionResult = await controller.SearchByAuthorAndAvailabilityAsync(author, availabilityStatus);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(fakeLibraryItemBasicDTOs);
        }


    }
}
