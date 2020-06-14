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
    public class MaintenanceHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MaintenanceHistoryController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find maintenance history
            var maintenanceHistory = await _uow.MaintenanceHistories.GetWithAllRelationsAsync(id);

            if (maintenanceHistory != null && maintenanceHistory.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<MaintenanceHistorySharedDto>(maintenanceHistory));
            else
                return NotFound(new { Success = false, Message = "Maintenance History not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get  maintenance history list
            var maintenanceHistoryList = await _uow.MaintenanceHistories.GetAllWithAllRelationsAsync();

            //if not empty, map  maintenance history list => MaintenanceHistorySharedDto list
            if (maintenanceHistoryList != null && maintenanceHistoryList.Count() > 0)
                return Ok(_mapper.Map<List<MaintenanceHistorySharedDto>>(maintenanceHistoryList));
            else
                return NotFound(new { Success = false, Message = "No Maintenance History found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] MaintenanceHistoryRequestDto maintenanceHistory)
        {
            //Before updating find maintenance history by id
            var maintenanceHistoryData = await _uow.MaintenanceHistories.GetAsync(maintenanceHistory.ID);

            if (maintenanceHistoryData != null && maintenanceHistoryData.ID > 0)
            {
                //Map update data
                _mapper.Map(maintenanceHistory, maintenanceHistoryData);

                //Change Modified Data
                maintenanceHistoryData.ModifyDate = DateTime.Now;

                _uow.MaintenanceHistories.Update(maintenanceHistoryData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                {
                    //get maintenance history proper data
                    var maintenanceHistoryDetails = await _uow.MaintenanceHistories.GetWithAllRelationsAsync(maintenanceHistory.ID);

                    //Before returning updated Maintenance History data, map Maintenacne History => MaintenanceHistorySharedDto
                    return Ok(_mapper.Map<MaintenanceHistorySharedDto>(maintenanceHistoryDetails));
                }
                else
                    return new JsonResult(new { Success = false, Message = "Maintenance History type changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Maintenance History not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<MaintenanceHistorySharedDto> Create([FromBody] MaintenanceHistoryRequestDto maintenanceHistory)
        {
            //Map dto Maintenance History object
            var mappedMaintenanceHistoryData = _mapper.Map<MaintenanceHistory>(maintenanceHistory);

            //Add not mapped fields
            mappedMaintenanceHistoryData.CreateDate = DateTime.Now;
            mappedMaintenanceHistoryData.ModifyDate = DateTime.Now;
            mappedMaintenanceHistoryData.IsDeleted = false;

            var maintenanceHistoryDetails =  await _uow.MaintenanceHistories.AddAsync(mappedMaintenanceHistoryData);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Get proper maintenance history data
                var properModelForMapping = await _uow.MaintenanceHistories.GetWithAllRelationsAsync(maintenanceHistoryDetails.ID);

                //Map Maintenance History => MaintenanceHistorySharedDto
                var resultVehicleType = _mapper.Map<MaintenanceHistorySharedDto>(properModelForMapping);

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
            //Before Deleting Maintenance History, first find Maintenance History details
            var maintenanceHistory = await _uow.MaintenanceHistories.GetAsync(id);

            if (maintenanceHistory != null && maintenanceHistory.ID > 0)
            {
                //Mark Maintenance History as deleted
                _uow.MaintenanceHistories.Remove(maintenanceHistory);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Maintenance History Type deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Maintenance History not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Maintenance History not found with given id." });
        }
    }
}
