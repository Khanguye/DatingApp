using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet()]

        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {

            // var users = await _userRepository.GetUsersAsync();
            // var returnUsers = _mapper.Map<IEnumerable<MemberDTO>>(users);
            var returnUsers = await _userRepository.GetMembersAsync();
            return Ok(returnUsers);
        }

        [HttpGet("{username}")]

        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            // var user = await _userRepository.GetUserByUsernameAsync(username);
            // var returnUser = _mapper.Map<MemberDTO>(user);
            var returnUser = await _userRepository.GetMemberByUsernameAsync(username);

            return Ok(returnUser);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO){

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDTO, user);

            _userRepository.Update(user);

            if( await _userRepository.SaveAllAsync() ) return NoContent();

            return BadRequest("Failed to update user");

        }
    }
}