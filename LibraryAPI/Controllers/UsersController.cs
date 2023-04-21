using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBorrowTransactionRepository _borrowTransactionRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IBorrowTransactionRepository borrowTransactionRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _borrowTransactionRepository = borrowTransactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(usersDTO);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("No User Found");
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UpdateUserDTO createUserDTO)
        {
            User user = _mapper.Map<User>(createUserDTO);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return CreatedAtRoute("GetLibraryItemById", new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateUserDTO updateUserDTO)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map( updateUserDTO , user );
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            UserDTO updatedUserDTO = _mapper.Map<UserDTO>(user);
            var response = new UpdateUserResponse
            {
                User = updatedUserDTO,
                Message = "Library item updated successfully."
            };

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }


                bool hasTransactions = await _borrowTransactionRepository.UserHasTransactionsAsync(id);
                if (hasTransactions)
                {
                    return BadRequest("Cannot delete user with related transactions.");
                }


            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully." });
        }




    }

    public class UpdateUserResponse
    {
        public UserDTO User { get; set; } = new UserDTO();
        public string Message { get; set; } = string.Empty;
    }
}
