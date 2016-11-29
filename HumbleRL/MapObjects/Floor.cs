using Microsoft.Xna.Framework;
using SadConsole;

namespace HumbleRL.MapObjects
{
	class Floor : MapObjectBase
	{
		public Floor() : base( Color.DarkGray, Color.Transparent, 46 )
		{
			Info.BlocksMove = false;
			Info.IsExplored = false;
			Info.IsTransparent = true;

			Info.Description = "Just a Floor";
		}
	}
}
