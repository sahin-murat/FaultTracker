using AutoMapper;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Business.Interfaces;
using FaultTracker.Business.Services;
using FaultTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            //Find user
            var user = await _uow.Users.GetAsync(id);

            if (user != null && user.ID > 0)
                //Map as to Dto object
                return Ok(_mapper.Map<UserSharedDto>(user));
            else
                return NotFound();
        }
    }
}
