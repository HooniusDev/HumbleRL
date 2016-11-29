using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.Consoles
{

	class MessagesConsole : SadConsole.Consoles.Console
	{

		private static MessagesConsole instance;

		private MessagesConsole( int width, int height ) : base (width, height) { }

		private string lastMessage = string.Empty;

		public static MessagesConsole Instance
		{
			get
			{
				if ( instance == null )
				{
					instance = new MessagesConsole( 80, 6 ); 
				}
				return instance;
			}
		}

		//public MessagesConsole( int width, int height ) : base( width, height )
		//{
		//	GameWorld.MessageConsole = this;
		//	PrintMessage( "works!" );
		//}

		public void PrintMessage( string text )
		{
			if ( text != lastMessage )
			{
				ShiftDown( 1 );
				VirtualCursor.Print( text ).CarriageReturn();
				lastMessage = text;
			}
		}

		public void PrintMessage( ColoredString text )
		{
			if ( text.ToString() != lastMessage )
			{
				ShiftDown( 1 );
				VirtualCursor.Print( text ).CarriageReturn();
				lastMessage = text.ToString();
			}
		}
	}

}

