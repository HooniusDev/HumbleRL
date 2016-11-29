using Microsoft.Xna.Framework;
using SadConsole;

namespace HumbleRL.MapObjects
{
	class StairsUp : MapObjectBase
	{
		public StairsUp() : base( Color.DarkGray, Color.Transparent, 60 )
		{
			Info.BlocksMove = false;
			Info.IsExplored = false;
			Info.IsTransparent = true;

			Info.Description = "Staircase going up.";
		}
	}
}