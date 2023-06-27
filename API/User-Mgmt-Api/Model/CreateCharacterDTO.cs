namespace User_Mgmt_Api.Model
{
	public class CreateCharacterDTO
	{
		public string Name { get; set; }
		public string RpgClass { get; set; }
		public Guid userId { get; set; }

	}
}
