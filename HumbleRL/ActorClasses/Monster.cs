using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.ActorClasses
{
	class Monster : ActorBase
	{

		public bool InFov;

		public Monster() : base()
		{
			Appearance = new CellAppearance( Color.GreenYellow, Color.TransparentBlack, 'T' );
			Info = new ActorInfo( "Generic Troll", 0, 4 );
		}


	}
}
