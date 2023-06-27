using System.Text.Json.Serialization;

namespace User_Mgmt_Api.Model
{
	public class Character
	{
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string RpgClass { get; set; } = "Knight";
        [JsonIgnore]
        public User user { get; set; }
        public Guid Userid { get; set; }
        [JsonIgnore]
        public Weapon weapon { get; set; }
        public int? weaponId { get; set; }
        public List<Skill> skills { get; set; }
    }
}
