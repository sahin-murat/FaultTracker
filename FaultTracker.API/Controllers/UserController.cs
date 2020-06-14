using FaultTracker.Business.Interfaces;
using FaultTracker.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaultTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        public UserController(IUnitOfWork unitOfWork) => _uow = unitOfWork as UnitOfWork;

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _uow.Users.GetAsync(id);

            if (user != null && user.ID > 0)
                //As userResponsoDto
                return Ok(user);
            else
                return NotFound();
        }
    }
}
