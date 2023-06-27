using System.Text.Json.Serialization;

namespace User_Mgmt_Api.Model
{
	public class Weapon
	{
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Damage { get; set; }
        [JsonIgnore]
        public Character character { get; set; }
        public int? characterId { get; set; }
    }
}
