using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //Get vehicle type list
            var vehicleTypeList = await _uow.VehicleTypes.GetAllAsync();

            //if not empty, map vehicle type list => VehicleTypeSharedDto list
            if (vehicleTypeList != null && vehicleTypeList.Count() > 0)
                return Ok(_mapper.Map<List<VehicleTypeSharedDto>>(vehicleTypeList));
            else
                return NotFound(new { Success = false, Message = "No Vehicle Type found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] VehicleTypeRequestDto vehicleType)
        {
            //Before updating find vehicle type by id
            var vehicleTypeData = await _uow.VehicleTypes.GetAsync(vehicleType.ID);

            if (vehicleTypeData != null && vehicleTypeData.ID > 0)
            {
                //Map update data
                _mapper.Map(vehicleType, vehicleTypeData);

                //Change Modified Data
                vehicleTypeData.ModifyDate = DateTime.Now;

                _uow.VehicleTypes.Update(vehicleTypeData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    //Before returning updated vehicle type data, map VehicleType => VehicleTypeSharedDto
                    return Ok(_mapper.Map<VehicleTypeSharedDto>(vehicleTypeData));
                else
                    return new JsonResult(new { Success = false, Message = "Vehicle type changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Vehicle type not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<VehicleTypeSharedDto> Create([FromBody] VehicleTypeRequestDto vehicleType)
        {
            //Map dto Vehicle Type object
            var mappedVehicleType = _mapper.Map<VehicleType>(vehicleType);

            //Add not mapped fields
            mappedVehicleType.CreateDate = DateTime.Now;
            mappedVehicleType.ModifyDate = DateTime.Now;
            mappedVehicleType.IsDeleted = false;

            await _uow.VehicleTypes.AddAsync(mappedVehicleType);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Map Vehicle Type => VehicleTypeSharedDto
                var resultVehicleType = _mapper.Map<VehicleTypeSharedDto>(mappedVehicleType);

                return resultVehicleType;
            }
            else
                return null;
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Before Deleting vehicle type, first find vehicle type details
            var vehicleType = await _uow.VehicleTypes.GetAsync(id);

            if (vehicleType != null && vehicleType.ID > 0)
            {
                //Mark vehicle type as deleted
                _uow.VehicleTypes.Remove(vehicleType);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Vehicle Type deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Vehicle Type not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Vehicle Type not found with given id." });
        }
    }
}
