using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ActionTypeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ActionTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find Action type
            var actionType = await _uow.ActionTypes.GetAsync(id);

            if (actionType != null && actionType.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<ActionTypeSharedDto>(actionType));
            else
                return NotFound(new { Success = false, Message = "Action Type not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get vehicle type list
            var actionTypeList = await _uow.ActionTypes.GetAllAsync();

            //if not empty, map Action type list => ActionTypeSharedDto list
            if (actionTypeList != null && actionTypeList.Count() > 0)
                return Ok(_mapper.Map<List<ActionTypeSharedDto>>(actionTypeList));
            else
                return NotFound(new { Success = false, Message = "No Action Type found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ActionTypeRequestDto actionType)
        {
            //Before updating find Action type by id
            var actionTypeData = await _uow.ActionTypes.GetAsync(actionType.ID);

            if (actionTypeData != null && actionTypeData.ID > 0)
            {
                //Map update data
                _mapper.Map(actionType, actionTypeData);

                //Change Modified Data
                actionTypeData.ModifyDate = DateTime.Now;

                _uow.ActionTypes.Update(actionTypeData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    //Before returning updated Action type data, map ActionType => ActionTypeSharedDto
                    return Ok(_mapper.Map<ActionTypeSharedDto>(actionTypeData));
                else
                    return new JsonResult(new { Success = false, Message = "Action type changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Action type not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionTypeSharedDto> Create([FromBody] ActionTypeRequestDto actionType)
        {
            //Map dto Action Type object
            var mappedActionTypeData = _mapper.Map<ActionType>(actionType);

            //Add not mapped fields
            mappedActionTypeData.CreateDate = DateTime.Now;
            mappedActionTypeData.ModifyDate = DateTime.Now;
            mappedActionTypeData.IsDeleted = false;

            await _uow.ActionTypes.AddAsync(mappedActionTypeData);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Map Action Type => ActionTypeSharedDto
                var resultVehicleType = _mapper.Map<ActionTypeSharedDto>(mappedActionTypeData);

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
            //Before Deleting Action type, first find Action type details
            var actionType = await _uow.ActionTypes.GetAsync(id);

            if (actionType != null && actionType.ID > 0)
            {
                //Mark Action type as deleted
                _uow.ActionTypes.Remove(actionType);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Action Type deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Action Type not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Action Type not found with given id." });
        }
    }
}
