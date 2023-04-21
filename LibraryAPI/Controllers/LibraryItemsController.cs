using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryItemsController : ControllerBase
    {
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly IBorrowTransactionRepository _borrowTransactionRepository;
        private readonly IMapper _mapper;

        public LibraryItemsController(ILibraryItemRepository libraryItemRepository, IBorrowTransactionRepository borrowTransactionRepository, IMapper mapper)
        {
            _libraryItemRepository = libraryItemRepository;
            _borrowTransactionRepository = borrowTransactionRepository;
            _mapper = mapper;
        }
        //Get All Books
        [HttpGet]
        [SwaggerOperation("Get All Books")]
        public async Task<ActionResult<IEnumerable<LibraryItemDTO>>> GetAllAsync()
        {
            var libraryItems = await _libraryItemRepository.GetAllAsync();
            var libraryItemsDTO = _mapper.Map<IEnumerable<LibraryItemDTO>>(libraryItems);
            return Ok(libraryItemsDTO);
        }

        [HttpGet("{id}", Name = "GetLibraryItemById")]
        public async Task<ActionResult<LibraryItemDTO>> GetByIdAsync(int id)
        {
            var libraryItem = await _libraryItemRepository.GetByIdAsync(id);

            if (libraryItem == null)
            {
                return NotFound();
            }

            var libraryItemDTO = _mapper.Map<LibraryItemDTO>(libraryItem);
            return Ok(libraryItemDTO);
        }

        [HttpGet("search")]
        [SwaggerOperation("Search Library Items by Title")]
        public async Task<IActionResult> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title must not be empty.");
            }

            IEnumerable<LibraryItem> libraryItems = await _libraryItemRepository.SearchByTitleAsync(title);

            if (!libraryItems.Any())
            {
                return NotFound("No items found with the given title.");
            }

            return Ok(_mapper.Map<IEnumerable<LibraryItemBasicDTO>>(libraryItems));
        }

        //Search Books by Title.
        [HttpGet("search/book")]
        [SwaggerOperation("Search Books by Title")]
        public async Task<IActionResult> SearchBooksByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title must not be empty.");
            }

            IEnumerable<LibraryItem> libraryItems = await _libraryItemRepository.SearchByTitleAndTypeAsync(title, ItemType.Book);

            if (!libraryItems.Any())
            {
                return NotFound("No books found with the given title.");
            }

            return Ok(_mapper.Map<IEnumerable<LibraryItemBasicDTO>>(libraryItems));
        }

        //Search Items by Author and Availability Status
        [HttpGet("search/author-availability")]
        [SwaggerOperation("Search Items by Author and Availability Status")]
        public async Task<ActionResult<IEnumerable<LibraryItemBasicDTO>>> SearchByAuthorAndAvailabilityAsync(string author, AvailabilityStatus availabilityStatus)
        {
            var libraryItems = await _libraryItemRepository.SearchByAuthorAndAvailabilityAsync(author, availabilityStatus);
            var libraryItemsDto = _mapper.Map<IEnumerable<LibraryItemBasicDTO>>(libraryItems);

            return Ok(libraryItemsDto);
        }

        //Search Books by Author and Availability Status
        [HttpGet("search/author-availability-books")]
        [SwaggerOperation("Search Books by Author and Availability Status")]
        public async Task<ActionResult<IEnumerable<LibraryItemBasicDTO>>> SearchBooksByAuthorAndAvailabilityAsync(string author, AvailabilityStatus availabilityStatus)
        {

            var libraryItems = await _libraryItemRepository.SearchByAuthorAndAvailabilityAndTypeAsync(author, availabilityStatus, ItemType.Book);
            if (!libraryItems.Any())
            {
                return NotFound("No books found with the given Author.");
            }
            var libraryItemsDto = _mapper.Map<IEnumerable<LibraryItemBasicDTO>>(libraryItems);

            return Ok(libraryItemsDto);
        }



        [HttpPost]
        public async Task<IActionResult> AddAsync(LibraryItemBasicDTO libraryItemBasicDTO)
        {
            LibraryItem libraryItem = _mapper.Map<LibraryItem>(libraryItemBasicDTO);

            await _libraryItemRepository.AddAsync(libraryItem);
            await _libraryItemRepository.SaveChangesAsync();

            return CreatedAtRoute("GetLibraryItemById", new { id = libraryItem.ItemID }, libraryItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, LibraryItemBasicDTO libraryItemBasicDTO)
        {
            LibraryItem libraryItem = await _libraryItemRepository.GetByIdAsync(id);

            if (libraryItem == null)
            {
                return NotFound();
            }

            _mapper.Map(libraryItemBasicDTO, libraryItem);
            _libraryItemRepository.Update(libraryItem);
            await _libraryItemRepository.SaveChangesAsync();

            LibraryItemDTO updatedLibraryItemDTO = _mapper.Map<LibraryItemDTO>(libraryItem);
            var response = new UpdateLibraryItemResponse
            {
                LibraryItem = updatedLibraryItemDTO,
                Message = "Library item updated successfully."
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var libraryItem = await _libraryItemRepository.GetByIdAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }
                bool hasTransactions = await _borrowTransactionRepository.LibraryItemHasTransactionsAsync(id);
                if (hasTransactions)
                {
                    return BadRequest("Cannot delete library item with related transactions.");
                }



            _libraryItemRepository.Delete(libraryItem);
            await _libraryItemRepository.SaveChangesAsync();

            return Ok(new { message = "Library item deleted successfully." });
        }

    }


    public class UpdateLibraryItemResponse
    {
        public LibraryItemDTO LibraryItem { get; set; } = new LibraryItemDTO();
        public string Message { get; set; } = string.Empty;
    }
}

