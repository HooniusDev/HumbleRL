using Microsoft.Xna.Framework;
using SadConsole;

namespace HumbleRL.MapObjects
{
	class Wall : MapObjectBase
	{
		public Wall() : base( Color.White, Color.Transparent, 35 )
		{
			Info.BlocksMove = true;
			Info.IsExplored = false;
			Info.IsTransparent = false;

			Info.Description = "Rock solid wall.";
		}

	}
}
