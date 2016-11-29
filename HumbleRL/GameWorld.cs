using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumbleRL.Consoles;

namespace HumbleRL
{
	static class GameWorld
	{
		public static DungeonScreen DungeonScreen;

		public static void Start()
		{
			DungeonScreen = new DungeonScreen();
			SadConsole.Engine.ConsoleRenderStack.Add( DungeonScreen );
			MessagesConsole.Instance.PrintMessage( "Welcome to the RL..." );
		}

	}
}
