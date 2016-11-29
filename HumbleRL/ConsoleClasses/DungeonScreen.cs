using HumbleRL.ActorClasses;
using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using SadConsole.Input;
using System;
using Console = SadConsole.Consoles.Console;

namespace HumbleRL.Consoles
{
/// <summary>
/// Groups all consoles together
/// </summary>
	class DungeonScreen : ConsoleList
	{

		public DungeonMapConsole ViewConsole;
		public CharacterStatusPanel StatsConsole;
		public MessagesConsole MessageConsole;

		private Console messageHeaderConsole;	

		public DungeonScreen() 
		{
			StatsConsole = new CharacterStatusPanel( 24, 41 );
			ViewConsole = new DungeonMapConsole( this, 56, 42, 80,80 );
			MessageConsole = MessagesConsole.Instance; // = new MessagesConsole( ); // defaults to 80,6

			// Setup Input
			CanUseKeyboard = true;
			SadConsole.Engine.ActiveConsole = this;

			SadConsole.Engine.Keyboard.RepeatDelay = 0.07f;
			SadConsole.Engine.Keyboard.InitialRepeatDelay = 0.1f;

			messageHeaderConsole = new Console( 80, 1 );
			messageHeaderConsole.DoUpdate = false;
			messageHeaderConsole.CanUseKeyboard = false;
			messageHeaderConsole.CanUseMouse = false;

			// Draw the line for the header
			messageHeaderConsole.Fill( Color.White, Color.Black, 196, null );
			messageHeaderConsole.SetGlyph( 56, 0, 193 ); // This makes the border match the character console's left-edge border

			// Print the header text
			messageHeaderConsole.Print( 2, 0, " Messages " );

			// Move the rest of the consoles into position (ViewConsole is already in position at 0,0)
			StatsConsole.Position = new Point( 56, 0 );
			MessageConsole.Position = new Point( 0, 42 );
			messageHeaderConsole.Position = new Point( 0, 41 );

			// Add all consoles to this console list.
			Add( messageHeaderConsole );
			Add( StatsConsole );
			Add( ViewConsole );
			Add( MessageConsole );

		}

		public void HookUpHero( Hero hero )
		{
			StatsConsole.hookUpListener( hero );
		}

		public override bool ProcessKeyboard( KeyboardInfo info )
		{

			if ( info.KeysPressed.Contains( AsciiKey.Get( Microsoft.Xna.Framework.Input.Keys.Down ) ) )
			{
				ViewConsole.Walk( new Point( 0, 1 ) );
			}
			else if ( info.KeysPressed.Contains( AsciiKey.Get( Microsoft.Xna.Framework.Input.Keys.Up ) ) )
			{
				ViewConsole.Walk( new Point( 0, -1 ));
			}

			if ( info.KeysPressed.Contains( AsciiKey.Get( Microsoft.Xna.Framework.Input.Keys.Right ) ) )
			{
				ViewConsole.Walk( new Point( 1, 0 ));
			}
			else if ( info.KeysPressed.Contains( AsciiKey.Get( Microsoft.Xna.Framework.Input.Keys.Left ) ) )
			{
				ViewConsole.Walk( new Point( -1, 0 ));
			}
	
			return false;
		}


	}
}
