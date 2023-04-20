using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryItemsController : ControllerBase
    {
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly IMapper _mapper;

        public LibraryItemsController(ILibraryItemRepository libraryItemRepository, IMapper mapper)
        {
            _libraryItemRepository = libraryItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
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
        //search Items by title.
        [HttpGet("search")]
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

        [HttpGet("search/author-availability")]
        public async Task<ActionResult<IEnumerable<LibraryItemBasicDTO>>> SearchByAuthorAndAvailabilityAsync(string author, AvailabilityStatus availabilityStatus)
        {
            var libraryItems = await _libraryItemRepository.SearchByAuthorAndAvailabilityAsync(author, availabilityStatus);
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

            _libraryItemRepository.Delete(libraryItem);
            await _libraryItemRepository.SaveChangesAsync();

            return NoContent();
        }

    }

    public class UpdateLibraryItemResponse
    {
        public LibraryItemDTO LibraryItem { get; set; }
        public string Message { get; set; }
    }
}