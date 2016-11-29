using HumbleRL.ActorClasses;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.Consoles
{
/// <summary>
/// Display character status
/// </summary>
	class CharacterStatusPanel : SadConsole.Consoles.Console
	{

		public int maxHealth = 100;
		public int health = 40;

		public CharacterStatusPanel( int width, int height ) : base( width, height )
		{
			SadConsole.Shapes.Line line = new SadConsole.Shapes.Line();
			line.EndingLocation = new Point( 0, height - 1 );
			line.CellAppearance.GlyphIndex = 179;
			line.UseEndingCell = false;
			line.UseStartingCell = false;
			line.Draw( this );
		}

		public void hookUpListener( Hero hero )
		{
			hero.Changed += new Hero.ChangedEventHandler( RedrawPanel );
		}

		private void RedrawPanel( Hero hero )
		{
			Print( 2, 2, hero.Info.Name );
			Print( 2, 3, hero.Position.ToString() );

			SadConsole.ColoredString healthStatus = health.ToString().CreateColored													( Color.LightGreen, Color.Black, null ) +											"/".CreateColored( Color.White,														Color.Black, null ) +																maxHealth.ToString().CreateColored													( Color.DarkGreen, Color.Black,	null );
			Print( Width - 2 - healthStatus.ToString().Length, 2, healthStatus );
		}



	}
}
