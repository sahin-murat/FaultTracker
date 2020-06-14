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
    public class MaintenanceController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MaintenanceController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find Maintenance
            var maintenanceDetails = await _uow.Maintenances.GetWithAllRelationsAsync(id);

            if (maintenanceDetails != null && maintenanceDetails.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<MaintenanceSharedDto>(maintenanceDetails));
            else
                return NotFound(new { Success = false, Message = "Maintenance details not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get Maintenance list
            var maintenanceList = await _uow.Maintenances.GetAllWithAllRelationsAsync();

            //if not empty, map Maintenance list => MaintenanceSharedDto list
            if (maintenanceList != null && maintenanceList.Count() > 0)
                return Ok(_mapper.Map<List<MaintenanceSharedDto>>(maintenanceList));
            else
                return NotFound(new { Success = false, Message = "No Maintenance found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] MaintenanceRequestDto maintenance)
        {
            //Before updating find vehicle type by id
            var maintenanceData = await _uow.Maintenances.GetAsync(maintenance.ID);

            if (maintenanceData != null && maintenanceData.ID > 0)
            {
                //Map update data
                _mapper.Map(maintenance, maintenanceData);

                //Change Modified Data
                maintenanceData.ModifyDate = DateTime.Now;

                _uow.Maintenances.Update(maintenanceData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                {
                    //Get Maintenance Proper data 
                    var maintenanceDetails = await _uow.Maintenances.GetWithAllRelationsAsync(maintenance.ID);

                    //Before returning updated Maintenance data, map Maintenance => MaintenanceSharedDto
                    return Ok(_mapper.Map<MaintenanceSharedDto>(maintenanceDetails));
                }
                else
                    return new JsonResult(new { Success = false, Message = "Maintenance changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Maintenance not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<MaintenanceSharedDto> Create([FromBody] MaintenanceRequestDto maintenance)
        {
            //To Do check ID's for preventing errors

            //Map dto Maintenance object
            var mappedMaintenanceData = _mapper.Map<Maintenance>(maintenance);

            //Add not mapped fields
            mappedMaintenanceData.CreateDate = DateTime.Now;
            mappedMaintenanceData.ModifyDate = DateTime.Now;
            mappedMaintenanceData.IsDeleted = false;

            var maintenanceDetails = await _uow.Maintenances.AddAsync(mappedMaintenanceData);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Get Maintenance Proper data 
                var maintenanceProperData = await _uow.Maintenances.GetWithAllRelationsAsync(maintenanceDetails.ID);

                //Map Maintenance => MaintenanceSharedDto
                var resultVehicleType = _mapper.Map<MaintenanceSharedDto>(maintenanceProperData);

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
            //Before Deleting Maintenance, first find Maintenance details
            var maintenance = await _uow.Maintenances.GetAsync(id);

            if (maintenance != null && maintenance.ID > 0)
            {
                //Mark Maintenance as deleted
                _uow.Maintenances.Remove(maintenance);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Maintenance Type deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Maintenance Type not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Maintenance Type not found with given id." });
        }

    }
}
