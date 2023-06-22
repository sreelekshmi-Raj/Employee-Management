using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Mgmt_Api.Model;
using User_Mgmt_Api.Model.DTO;
using User_Mgmt_Api.Repository;

namespace User_Mgmt_Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await userRepository.GetAllAsync();
            return Ok(users);
        }
		//in token no role as claim then 403 forbidden
		//in token role is diff then 403 forbidden
		[HttpGet,Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var user=await userRepository.GetByIdAsync(id);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateUser userDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var UserDb = new User
            {
               
                Name= userDTO.Name,
                Email=userDTO.Email,
                City=userDTO.City,
                Region=userDTO.Region,
                PostalCode=userDTO.PostalCode,
                Country=userDTO.Country,
                Phone=userDTO.Phone
			};
            var users =await userRepository.InsertAsync(UserDb);
            return Ok(users);
        }
        [HttpPut]
        public async Task<ActionResult> Update(Guid id, UpdateUser userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new User
            {
                Name = userDTO.Name,
                Email=userDTO.Email,
                City=userDTO.City,
                Region=userDTO.Region,
                PostalCode=userDTO.PostalCode,
                Country=userDTO.Country,
                Phone=userDTO.Phone

            };
            var userDb=await userRepository.UpdateAsync(id, user);
            return Ok(userDb);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userDb=await userRepository.DeleteAsync(id);
            if (userDb == null)
                return BadRequest();
            return Ok(userDb);
        }

    }
}
