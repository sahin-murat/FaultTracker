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
    public class PictureGroupController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PictureGroupController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find Picture Froup
            var pictureGroupData = await _uow.PictureGroups.GetAsync(id);

            if (pictureGroupData != null && pictureGroupData.ID > 0)
                //Map to Dto object and return
                return Ok(_mapper.Map<PictureGroupSharedDto>(pictureGroupData));
            else
                return NotFound(new { Success = false, Message = "Picture Group details not found with given id." });
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //Get Picture Group list
            var pictureGroupList = await _uow.PictureGroups.GetAllAsync();

            //if not empty, map Picture Group list => PictureGroupSharedDto list
            if (pictureGroupList != null && pictureGroupList.Count() > 0)
                return Ok(_mapper.Map<List<PictureGroupSharedDto>>(pictureGroupList));
            else
                return NotFound(new { Success = false, Message = "No Picture Group found in db." });
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PictureGroupRequestDto pictureGroup)
        {
            //Before updating find Picture Group by id
            var pictureGroupData = await _uow.PictureGroups.GetAsync(pictureGroup.ID);

            if (pictureGroupData != null && pictureGroupData.ID > 0)
            {
                //Map update data
                _mapper.Map(pictureGroup, pictureGroupData);

                //Change Modified Data
                pictureGroupData.ModifyDate = DateTime.Now;

                _uow.PictureGroups.Update(pictureGroupData);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    //Before returning updated Picture Group data, map PictureGroup => PictureGroupSharedDto
                    return Ok(_mapper.Map<PictureGroupSharedDto>(pictureGroupData));
                else
                    return new JsonResult(new { Success = false, Message = "Picture Group changes are not updated" });
            }
            else
                return NotFound(new { Success = false, Message = "Picture Group not found with sended details." });
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<PictureGroupSharedDto> Create([FromBody] PictureGroupRequestDto pictureGroup)
        {
            //Map dto Picture Group object
            var mappedPictureGroupData = _mapper.Map<PictureGroup>(pictureGroup);

            //Add not mapped fields
            mappedPictureGroupData.CreateDate = DateTime.Now;
            mappedPictureGroupData.ModifyDate = DateTime.Now;
            mappedPictureGroupData.IsDeleted = false;

            var pictureGroupDetails = await _uow.PictureGroups.AddAsync(mappedPictureGroupData);
            var result = await _uow.CompleteAsync();

            if (result > 0)
            {
                //Map PictureGroup => PictureGroupSharedDto
                var resultVehicleType = _mapper.Map<PictureGroupSharedDto>(pictureGroupDetails);

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
            //Before Deleting Picture Group, first find Picture Group details
            var pictureGroup = await _uow.PictureGroups.GetAsync(id);

            if (pictureGroup != null && pictureGroup.ID > 0)
            {
                //Mark Picture Group as deleted
                _uow.PictureGroups.Remove(pictureGroup);
                var result = await _uow.CompleteAsync();

                if (result > 0)
                    return Ok(new { Success = true, Message = "Picture Group deleted successfully" });
                else
                    return new JsonResult(new { Success = false, Message = "Picture Group not deleted. Update is not successfull." });
            }
            else
                return NotFound(new { Success = false, Message = "Picture Group not found with given id." });
        }

    }
}
