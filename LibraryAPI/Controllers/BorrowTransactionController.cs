using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowTransactionsController : ControllerBase
    {
        private readonly IBorrowTransactionRepository _borrowTransactionRepository;
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        private readonly ILibraryItemRepository _libraryItemRepository;

        public BorrowTransactionsController(IBorrowTransactionRepository borrowTransactionRepository, IUserRepository userRepository, ILibraryItemRepository libraryItemRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _libraryItemRepository = libraryItemRepository;
            _borrowTransactionRepository = borrowTransactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowTransactionDTO>>> GetAllAsync()
        {
            var borrowTransactions = await _borrowTransactionRepository.GetAllAsync();
            var borrowTransactionDTOs = _mapper.Map<IEnumerable<BorrowTransactionDTO>>(borrowTransactions);
            return Ok(borrowTransactionDTOs);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<ActionResult<BorrowTransactionDTO>> GetByIdAsync(int id)
        {
            var borrowTransaction = await _borrowTransactionRepository.GetByIdAsync(id);

            if (borrowTransaction == null)
            {
                return NotFound();
            }

            var borrowTransactionDTO = _mapper.Map<BorrowTransactionDTO>(borrowTransaction);
            return Ok(borrowTransactionDTO);
        }
        //Assign a Book to a User
        [HttpPost("assign-book")]
        [SwaggerOperation("Assign a Book to a User")]
        public async Task<ActionResult<object>> AssignBookAsync(AssignBookDTO assignBookDTO)
        {
            var user = await _userRepository.GetByIdAsync(assignBookDTO.UserID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var libraryItem = await _libraryItemRepository.GetByIdAsync(assignBookDTO.ItemID);
            if (libraryItem == null)
            {
                return NotFound("Library item not found.");
            }

            if (libraryItem.AvailabilityStatus != AvailabilityStatus.Available)
            {
                return BadRequest("The library item is not available.");
            }

            var borrowTransaction = new BorrowTransaction
            {
                UserID = assignBookDTO.UserID,
                ItemID = assignBookDTO.ItemID,
                BorrowDate = assignBookDTO.BorrowDate,
                DueDate = assignBookDTO.DueDate
            };

            await _borrowTransactionRepository.AddAsync(borrowTransaction);
            await _borrowTransactionRepository.SaveChangesAsync();

            libraryItem.AvailabilityStatus = AvailabilityStatus.Borrowed;
            _libraryItemRepository.Update(libraryItem);
            await _libraryItemRepository.SaveChangesAsync();

            var borrowTransactionDTO = _mapper.Map<BorrowTransactionDTO>(borrowTransaction);
            var libraryItemDTO = _mapper.Map<LibraryItemDTO>(libraryItem);

            var response = new
            {
                transaction = borrowTransactionDTO,
                libraryItem = libraryItemDTO,
                message = "The book has been assigned to the user successfully."
            };

            return CreatedAtRoute("GetTransactionById", new { id = borrowTransaction.TransactionID }, response);
        }

        //Check The Borrowing History of a User
        [HttpGet("user/{userId}")]
        [SwaggerOperation("Check The Borrowing History of a User")]
        public async Task<ActionResult<IEnumerable<BorrowTransactionDTO>>> GetUserBorrowingHistory(int userId)
        {
            var borrowTransactions = await _borrowTransactionRepository.GetBorrowingHistoryByUserIdAsync(userId);
            var borrowTransactionDTOs = _mapper.Map<IEnumerable<BorrowTransactionDTO>>(borrowTransactions);
            return Ok(borrowTransactionDTOs);
        }


    }
}
