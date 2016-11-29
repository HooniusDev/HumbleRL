using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;

namespace HumbleRL.MapObjects
{
	public abstract class MapObjectBase
	{
		public CellAppearance Appearance { get; private set; }
		public ICellEffect EffectExplored { get; private set; }
		public ICellEffect EffectHidden { get; private set; }
		public ICellEffect EffectHighlight { get; private set; }

		public TileInfo Info { get; private set; }

		public MapObjectBase( Color foreground, Color background, int character)
		{
			Appearance = new CellAppearance( foreground, background, character);
			Info = new TileInfo();

			Info.IsInFOV = false;
			Info.IsExplored = false;

			Info.Description = "MapbjectBase";

			EffectExplored = new Recolor()
			{
				Foreground = Color.LightGray * 0.4f,
				Background = Color.LightGray * 0.1f,
				DoForeground = true,
				DoBackground = true,
				CloneOnApply = false
			};

			EffectHidden = new Recolor()
			{
				Foreground = Color.Black,
				Background = Color.Black,
				DoForeground = true,
				DoBackground = true,
				CloneOnApply = false
			};
		}

		public virtual void RenderToCell( SadConsole.Cell sadConsoleCell )
		{
			Appearance.CopyAppearanceTo( sadConsoleCell );

			// Clear out the old effect if there was one
			if ( sadConsoleCell.Effect != null )
			{
				sadConsoleCell.Effect.Clear( sadConsoleCell );
				sadConsoleCell.Effect = null;
			}

			if ( Info.IsInFOV )
			{
				// Do nothing if it's in view, it's a normal colored square
				// You could do something later like check how far the cell is from the player and tint it
			}

			if ( Info.IsExplored && !Info.IsInFOV )
			{
				sadConsoleCell.Effect = EffectExplored;
				sadConsoleCell.Effect.Apply( sadConsoleCell );
				return;
			}

			if ( !Info.IsExplored && !Info.IsInFOV )
			{
				RemoveCellFromView( sadConsoleCell );
				//sadConsoleCell.Effect = EffectHidden;
				//sadConsoleCell.Effect.Apply( sadConsoleCell );
				//sadConsoleCell.Effect.Apply( sadConsoleCell );
				return;
			}
		}
		public virtual void RemoveCellFromView( SadConsole.Cell sadConsoleCell )
		{
			// Clear out the old effect if there was one
			if ( sadConsoleCell.Effect != null )
			{
				sadConsoleCell.Effect.Clear( sadConsoleCell );
				sadConsoleCell.Effect = null;
				
			}
			sadConsoleCell.Reset();
			//sadConsoleCell.Effect = EffectSeen;
			//sadConsoleCell.Effect.Apply( sadConsoleCell );
		} 
	}
}
