using Microsoft.Xna.Framework;
using SadConsole;

namespace HumbleRL.MapObjects
{
	class Door : MapObjectBase, IOpenable
	{

		public bool Opened { get; private set; }

		public Door() : base( Color.White, Color.Transparent, 43 )
		{
			Info.BlocksMove = true;
			Info.IsExplored = false;
			Info.IsTransparent = false;

			Opened = false;
			Info.Description = "Closed Door";
		}

		public void Open() {
			Info.BlocksMove = false;
			Info.IsTransparent = true;
			Appearance.GlyphIndex = 96;

			Opened = true;
			Info.Description = "Here is a open door.";
		}

	}

	public interface IOpenable {
		void Open();
	}

}
