using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaultTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find vehicle type
            var vehicleType = await _uow.VehicleTypes.GetAsync(id);

            if (vehicleType != null && vehicleType.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<VehicleTypeSharedDto>(vehicleType));
            else
                return NotFound(new { Success = false, Message = "Vehicle Type not found with given id." });
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
    }
}
