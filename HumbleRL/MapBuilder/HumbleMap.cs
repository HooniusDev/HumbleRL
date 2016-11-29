using HumbleRL.ActorClasses;
using HumbleRL.MapObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.MapBuilder
{
	class HumbleMap
	{

		int mapID;

		public List<Rectangle> roomRects;

		public Dictionary<Point, MapObjectBase> Map = new Dictionary<Point, MapObjectBase>();
		Dictionary<Point, ActorBase> Actors = new Dictionary<Point, ActorBase>(); // one actor at a position

		List<Point> VisibleTiles = new List<Point>();

		public bool IsTransparent( Point tile )
		{
			if ( Map.ContainsKey( tile ) )
			{
				if ( Map[tile].Info.IsTransparent )
					return true;
				else
					return false;
			}
			else
				return false;

		}

		public bool IsInFOV( Point tile )
		{
			if ( Map.ContainsKey( tile ) )
			{
				if ( Map[tile].Info.IsInFOV )
					return true;
				else
					return false;
			}
			else
				return false;

		}

		public bool IsEplored( Point tile )
		{
			if ( Map.ContainsKey( tile ) )
			{
				if ( Map[tile].Info.IsExplored )
					return true;
				else
					return false;
			}
			else
				return false;

		}

		public void SetInFOV( Point tile, bool fov = true )
		{
			if ( fov == true )
			{
				Map[tile].Info.IsInFOV = true;
				Map[tile].Info.IsExplored = true;
			}
			else
			{
				Map[tile].Info.IsInFOV = true;
			}
		}

		public void RenderTile( SadConsole.Consoles.Console console, Point tile )
		{
			Map[tile].RenderToCell( console[tile.X, tile.Y] );
		}

		public void SetTile( Point tile, TileTypes type )
		{
			switch ( type )
			{
				case TileTypes.Floor:
					{
						MapObjects.Floor floor = new MapObjects.Floor();
						Map.Add( tile, floor );
						break;
					}
				case TileTypes.Wall:
					{
						MapObjects.Wall wall = new MapObjects.Wall();
						Map.Add( tile, wall );
						break;
					}
				case TileTypes.Corridor:
					{
						MapObjects.Corridor corridor = new MapObjects.Corridor();
						Map.Add( tile, corridor );
						break;
					}
				case TileTypes.Door:
					{
						MapObjects.Door door = new MapObjects.Door();
						Map.Add( tile, door );
						break;
					}
				case TileTypes.None:
					{
						break;
					}
			}
		}

		public bool BlocksMove( Point tile )
		{
			if ( !Map.ContainsKey( tile ) || Map[tile].Info.BlocksMove )
				return true;
			else
				return false;
		}

		public string GetDescription(Point tile)
		{
			if ( Map.ContainsKey( tile ) )
				return Map[tile].Info.Description;
			else
				return "";
		}

		public HumbleMap( int width, int height )
		{
			csMapbuilder b = new csMapbuilder( width, height );
			b.Build_OneStartRoom();
			generateMapDictionary( b );
			roomRects = b.GetRooms();

		}

		private void generateMapDictionary( csMapbuilder b )
		{
			int[,] map = b.map;
			System.Console.WriteLine( "creating tiles" );
			for ( int x = 0; x < b.Map_Size.Width; x++ )
			{
				for ( int y = 0; y < b.Map_Size.Height; y++ )
				{
					int celltype = map[x, y];
					SetTile( new Point( x, y ), (TileTypes)celltype );

				}
			}
		}

		public enum TileTypes 
		{
			None = -1,
			Floor = 0,
			Wall = 1,
			Corridor = 2,
			Door = 3
		}
	}
}

