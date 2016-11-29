using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.ActorClasses
{
	public abstract class ActorBase
	{
		public CellAppearance Appearance { get; set; }
		public Point Position { get; set; }

		public ActorInfo Info { get; set; }

		public virtual void RenderToCell( Cell sadConsoleCell )
		{

			Appearance.CopyAppearanceTo( sadConsoleCell );
		}

		public virtual void UnRenderFromCell( Cell sadConsoleCell )
		{
			sadConsoleCell.Reset();

		}
	}

	public class ActorInfo
	{
		public string Name { get; private set; }

		public int Energy { get; set; }
		public int EnergyGainRate { get; private set; }

		public ActorInfo( string name, int energy = 0, int energyGainRate = 5 )
		{
			Name = name;
			Energy = energy;
			EnergyGainRate = energyGainRate;
		}
	}
}
