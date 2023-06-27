namespace User_Mgmt_Api.Model
{
	public class Skill
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Damage { get; set; }
		public List<Character> Character { get; set; }

	}
}
