using Microsoft.Xna.Framework;
using SadConsole;

namespace HumbleRL.MapObjects
{
	class Corridor : MapObjectBase
	{
		public Corridor() : base( Color.Gray, Color.Transparent, 44 )
		{
			Info.BlocksMove = false;
			Info.IsExplored = false;
			Info.IsTransparent = true;

			Info.Description = "Dark and damp corridor.";
		}

	}
}
