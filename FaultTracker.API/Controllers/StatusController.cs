using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaultTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StatusController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find Status
            var statusData = await _uow.Statuses.GetAsync(id);

            if (statusData != null && statusData.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<StatusSharedDto>(statusData));
            else
                return NotFound(new { Success = false, Message = "Status details not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get status list
            var statusList = await _uow.Statuses.GetAllAsync();

            //if not empty, map status list => StatusSharedDto list
            if (statusList != null && statusList.Count() > 0)
                return Ok(_mapper.Map<List<StatusSharedDto>>(statusList));
            else
                return NotFound(new { Success = false, Message = "No Status found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] StatusRequestDto status)
        {
            //Before updating find status by id
            var statusData = await _uow.Statuses.GetAsync(status.ID);

            if (statusData != null && statusData.ID > 0)
            {
                //Map update data
                _mapper.Map(status, statusData);

                //Change Modified Data
                statusData.ModifyDate = DateTime.Now;

                _uow.Statuses.Update(statusData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    //Before returning updated status data, map Status => StatusSharedDto
                    return Ok(_mapper.Map<StatusSharedDto>(statusData));
                else
                    return new JsonResult(new { Success = false, Message = "Status changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Status not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<StatusSharedDto> Create([FromBody] StatusRequestDto status)
        {
            if(status.ID > 0)
            {
                //check if status exist with id
                var tempStatusData =await _uow.Statuses.GetAsync(status.ID);

                //if not exist add it
                if(tempStatusData != null && tempStatusData.ID == 0)
                {
                    //Map dto Vehicle object
                    var mappedStatusData = _mapper.Map<Status>(status);

                    //Add not mapped fields
                    mappedStatusData.CreateDate = DateTime.Now;
                    mappedStatusData.ModifyDate = DateTime.Now;
                    mappedStatusData.IsDeleted = false;

                    var statusDetails = await _uow.Statuses.AddAsync(mappedStatusData);
                    var result = await _uow.CompleteAsync();

                    if (result > 0)
                    {
                        //Map Vehicle => VehicleSharedDto
                        var resultVehicleType = _mapper.Map<StatusSharedDto>(statusDetails);

                        return resultVehicleType;
                    }
                }
            }

            return null;                
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Before Deleting Status, first find status details
            var status = await _uow.Statuses.GetAsync(id);

            if (status != null && status.ID > 0)
            {
                //Mark status as deleted
                _uow.Statuses.Remove(status);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Status deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Status not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Status not found with given id." });
        }


    }
}
