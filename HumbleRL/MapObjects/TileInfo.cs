namespace HumbleRL.MapObjects
{
	public class TileInfo
	{
		public bool IsTransparent { get; set; }
		public bool BlocksMove { get; set; }
		public bool IsExplored { get; set; }
		public bool IsInFOV { get; set; }

		public string Description { get; set; }
	}
}