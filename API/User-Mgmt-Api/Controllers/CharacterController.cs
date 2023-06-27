using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Mgmt_Api.Data;

namespace User_Mgmt_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CharacterController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CharacterController(ApplicationDbContext applicationDbContext) {
			this.applicationDbContext = applicationDbContext;
		}
		[HttpGet]
		public async Task<ActionResult<List<Character>>> Get(Guid id)
		{
			//one to many =>one user having many characters
			var characters = await applicationDbContext.Characters
							.Where(c => c.Userid == id)
							.Include(c=>c.weapon)
							.ToListAsync();

			return characters;
		}

		[HttpPost]
		public async Task<ActionResult<List<Character>>> Create(CreateCharacterDTO newCharacter)
		{
			var user = await applicationDbContext.Users.FindAsync(newCharacter.userId);
			if (user == null)
				return NotFound();
			var request = new Character
			{
				Name = newCharacter.Name,
				RpgClass = newCharacter.RpgClass,
				Userid=newCharacter.userId
			};
			applicationDbContext.Characters.Add(request);
			applicationDbContext.SaveChanges();


			return await Get(newCharacter.userId);
		}
		[HttpPost("weapon")]
		public async Task<ActionResult<Character>> CreateWeapon(AddWeaponDTO newWeapon)
		{
			var characters = await applicationDbContext.Characters.FindAsync(newWeapon.CharacterId);
			if (characters == null)
				return NotFound();
			var request = new Weapon
			{
				Name = newWeapon.Name,
				Damage = newWeapon.Damage,
				characterId = newWeapon.CharacterId
			};
			applicationDbContext.Weapons.Add(request);
			applicationDbContext.SaveChanges();


			return  Ok(characters);
		}

	}
}
