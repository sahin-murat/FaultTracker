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
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleController(IUnitOfWork uow, IMapper mapper)
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
            var vehicleData = await _uow.Vehicles.GetVehicleWithRelationsAsync(id);

            if (vehicleData != null && vehicleData.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<VehicleSharedDto>(vehicleData));
            else
                return NotFound(new { Success = false, Message = "Vehicle not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get vehicle
            var vehicleList = await _uow.Vehicles.GetAllVehiclesWithRelationsAsync();

            //if not empty, map vehicle list => VehicleSharedDto list
            if (vehicleList != null && vehicleList.Count() > 0)
                return Ok(_mapper.Map<List<VehicleSharedDto>>(vehicleList));
            else
                return NotFound(new { Success = false, Message = "No Vehicle found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] VehicleRequestDto vehicle)
        {
            //Before updating find vehicle by id
            var vehicleData = await _uow.Vehicles.GetAsync(vehicle.ID);

            if (vehicleData != null && vehicleData.ID > 0)
            {
                //Map update data
                _mapper.Map(vehicle, vehicleData);

                //Change Modified Data
                vehicleData.ModifyDate = DateTime.Now;

                _uow.Vehicles.Update(vehicleData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                {
                    //Get Vehicle details with relation first
                    var properVehicleDetails = await _uow.Vehicles.GetVehicleWithRelationsAsync(vehicle.ID);

                    //Before returning updated vehicle data, map Vehicle => VehicleSharedDto
                    return Ok(_mapper.Map<VehicleSharedDto>(properVehicleDetails));
                }
                else
                    return new JsonResult(new { Success = false, Message = "Vehicle changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Vehicle not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<VehicleSharedDto> Create([FromBody] VehicleRequestDto vehicle)
        {
            //Map dto Vehicle object
            var mappedVehicleData = _mapper.Map<Vehicle>(vehicle);

            //Add not mapped fields
            mappedVehicleData.CreateDate = DateTime.Now;
            mappedVehicleData.ModifyDate = DateTime.Now;
            mappedVehicleData.IsDeleted = false;

            var vehicleDetails = await _uow.Vehicles.AddAsync(mappedVehicleData);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Get all vehicle details with relations
                var properVehicleDetails = await _uow.Vehicles.GetVehicleWithRelationsAsync(vehicleDetails.ID);

                //Map Vehicle => VehicleSharedDto
                var resultVehicleType = _mapper.Map<VehicleSharedDto>(properVehicleDetails);

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
            //Before Deleting vehicle, first find vehicle details
            var vehicle = await _uow.Vehicles.GetAsync(id);

            if (vehicle != null && vehicle.ID > 0)
            {
                //Mark vehicle as deleted
                _uow.Vehicles.Remove(vehicle);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Vehicle deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Vehicle not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Vehicle not found with given id." });
        }

    }
}
