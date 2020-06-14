using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Business.Interfaces;
using FaultTracker.Business.Services;
using FaultTracker.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaultTracker.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork as UnitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find user
            var user = await _uow.Users.GetAsync(id);

            if (user != null && user.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<UserSharedDto>(user));
            else
                return NotFound(new { Success = false, Message = "User not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get user list
            var userList = await _uow.Users.GetAllAsync();

            //if not empty, map user list => userSharedDto list
            if (userList != null && userList.Count() > 0)
                return Ok(_mapper.Map<List<UserSharedDto>>(userList));
            else
                return NotFound(new { Success = false, Message = "No user found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserRequestDto user)
        {
            //Before updating find user by id
            var userData = await _uow.Users.GetAsync(user.ID);

            if (userData != null && userData.ID > 0)
            {
                //Map update data
                _mapper.Map(user, userData);

                //Change Modified Data
                userData.ModifyDate = DateTime.Now;

                _uow.Users.Update(userData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    //Before returning updated user data, map user => UserSharedDto
                    return Ok(_mapper.Map<UserSharedDto>(userData));
                else
                    return new JsonResult(new { Success = false, Message = "User changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "User not found with sended details." });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<UserSharedDto> Create([FromBody] UserRequestDto user)
        {
            //Map dto user object
            var mappedUser = _mapper.Map<User>(user);

            //Add not mapped fields
            mappedUser.CreateDate = DateTime.Now;
            mappedUser.ModifyDate = DateTime.Now;
            mappedUser.IsDeleted = false;

            _uow.Users.AddAsync(mappedUser);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Map User => UserSharedDto
                var resultUser = _mapper.Map<UserSharedDto>(mappedUser);

                return resultUser;
            }
            else
                return null;
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Before Deleting user, first find user
            var user = await _uow.Users.GetAsync(id);

            if (user != null && user.ID > 0)
            {
                //Mark user as deleted
                _uow.Users.Remove(user);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "User deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "User not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "User not found with given id." });
        }
    }
}
