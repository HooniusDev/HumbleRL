using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.ActorClasses
{
	class Hero : ActorBase
	{
		public delegate void ChangedEventHandler( Hero hero );

		public event ChangedEventHandler Changed;

		public int SightRange;

		public Hero() : base()
		{
			Appearance = new CellAppearance( Color.White, Color.Black, '@' );
			Info = new ActorInfo( "Donk", 0, 7 );
			SightRange = 8;
		}

		public void OnChangedEvent()
		{
			if ( Changed != null )
				Changed( this );
		}

		//public void OnChanged()
		//{
		//	Changed( this );
		//}
	}
}
