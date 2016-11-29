using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;

namespace HumbleRL.MapObjects
{
	class GuiHighLight
	{

		public CellAppearance Appearance { get; private set; }

		public GuiHighLight(  )
		{
			Appearance = new CellAppearance( Color.Yellow, Color.LightYellow, 177 );
		}
		public void RenderToCell( SadConsole.Cell sadConsoleCell )
		{
			Appearance.CopyAppearanceTo( sadConsoleCell );
		}
		public void Clear( SadConsole.Cell sadConsoleCell )
		{
			sadConsoleCell.Reset();
		}
	}
}
