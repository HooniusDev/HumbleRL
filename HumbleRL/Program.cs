using System;
using Console = SadConsole.Consoles.Console;


namespace HumbleRL
{
	class Program
	{
		static void Main( string[] args )
		{
			// Setup the engine and creat the main window.
			SadConsole.Engine.Initialize( "Cheepicus12.font", 80, 50 );

			// Hook the start event so we can add consoles to the system.
			SadConsole.Engine.EngineStart += Engine_EngineStart;

			// Hook the update event that happens each frame so we can trap keys and respond.
			SadConsole.Engine.EngineUpdated += Engine_EngineUpdated;

			// Start the game.
			SadConsole.Engine.Run();
		}

		private static void Engine_EngineStart( object sender, EventArgs e )
		{
			// Clear the default console
			SadConsole.Engine.ConsoleRenderStack.Clear();
			SadConsole.Engine.ActiveConsole = null;

			GameWorld.Start();
		}

		private static void Engine_EngineUpdated( object sender, EventArgs e )
		{

		}
	}

}