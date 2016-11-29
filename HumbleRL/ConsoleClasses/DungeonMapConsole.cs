using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Game;
using SadConsole.Consoles;
using Console = SadConsole.Consoles.Console;
using System.Collections.Generic;
using System;
using HumbleRL.MapObjects;
using HumbleRL.ActorClasses;
using HumbleRL.Utils;
using System.Linq;
using HumbleRL.MapBuilder;

namespace HumbleRL.Consoles
{
	class DungeonMapConsole : Console
	{

		RogueSharp.Random.IRandom random = new RogueSharp.Random.DotNetRandom();

		DungeonScreen screen;

		private int _currentActor;

		public Hero hero = new Hero();
		public List<ActorBase> actors = new List<ActorBase>();

		Console ActorLayer;
		Console GuiLayer;

		MapBuilder.csMapbuilder builder;

		HumbleMap map;

		//My version of map..
		//Dictionary<Point, MapObjectBase> map.Map = new Dictionary<Point, MapObjectBase>();
		//Dictionary<Point, ActorBase> Actors = new Dictionary<Point, ActorBase>(); // one actor at a position
		List<Point> VisibleTiles = new List<Point>();

		int mapWidth;
		int mapHeight;


		public DungeonMapConsole( DungeonScreen screen, int viewWidth, int viewHeight, int mapWidth, int mapHeight ) : base( mapWidth, mapHeight )
		{
			TextSurface.RenderArea = new Rectangle( 0, 0, viewWidth, viewHeight );
			ActorLayer = new Console( mapWidth, mapHeight );
			GuiLayer = new Console( mapWidth, mapHeight );
			this.mapWidth = mapWidth;
			this.mapHeight = mapHeight;

			this.screen = screen;

			//GenerateBuilderMap();
			map = new HumbleMap( mapWidth, mapHeight );

			PlaceHero();
			PlaceMonsters( 15 );
			screen.HookUpHero( hero );
			hero.OnChangedEvent();
			UpdateFov();

		}

		private bool IsTransparent( Point tile )
		{
			return map.IsTransparent( tile );
				
		}

		private bool IsInFOV( Point tile )
		{
			return map.IsInFOV( tile );
		}

		private bool IsEplored( Point tile )
		{
			return map.IsEplored( tile );
		}

		private void SetInFOV( Point tile, bool fov = true )
		{
				map.SetInFOV( tile, fov );
				map.RenderTile( this, tile );
		}

		public override void Render()
		{
			base.Render();
			ActorLayer.Render();
			GuiLayer.Render();
		}

		public override void Update()
		{
			base.Update();
		}

		private void UpdateFov()
		{
			//Center view to Hero
			TextSurface.RenderArea = new Rectangle( hero.Position.X - ( TextSurface.RenderArea.Width / 2 ), hero.Position.Y - ( TextSurface.RenderArea.Height / 2 ), TextSurface.RenderArea.Width, TextSurface.RenderArea.Height );
			//Move ActorLayer with "parent"
			ActorLayer.TextSurface.RenderArea = TextSurface.RenderArea;

			//Tiles in previous version of FOV gets cleared
			foreach ( Point tile in VisibleTiles )
			{
				SetInFOV(tile, false );
			}

			VisibleTiles.Clear();
			//Generate new FOV
			#region CastRays

			Rectangle sightRect = new Rectangle( hero.Position.X - 50, hero.Position.Y - 50, 100, 100 );

			for ( int x = sightRect.X; x < sightRect.Right; x++ )
			{
				int y = sightRect.Y;  // TOP OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map.Map, hero.Position, new Point( x, y ), hero.SightRange ) );

				y = sightRect.Bottom; // BOTTOM OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map.Map, hero.Position, new Point( x, y ), hero.SightRange ) );
			}

			for ( int y = sightRect.Y; y < sightRect.Bottom; y++ )
			{
				int x = sightRect.X; // LEFT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map.Map, hero.Position, new Point( x, y ), hero.SightRange ) );

				x = sightRect.Right;  // RIGHT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map.Map, hero.Position, new Point( x, y ), hero.SightRange ) );
			}

			//to the right
			int distance = 0;
			int hideOpeningDistance = 3;
			for ( int x = hero.Position.X; x < hero.Position.X + hero.SightRange; x++ )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, hero.Position.Y + 1 );
				Point tileDown = new Point( x, hero.Position.Y - 1 );
				if ( !map.Map.ContainsKey( new Point( x, hero.Position.Y ) ) || !map.Map[new Point( x, hero.Position.Y )].Info.IsTransparent )
				{
					if ( map.Map.ContainsKey( tileUp ) && !map.Map[tileUp].Info.IsTransparent )
						VisibleTiles.Add( tileUp );
					if ( map.Map.ContainsKey( tileDown ) && !map.Map[tileDown].Info.IsTransparent )
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( map.Map.ContainsKey( tileUp ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileUp].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( map.Map.ContainsKey( tileDown ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileDown].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileDown );
						}
						else
							VisibleTiles.Add( tileDown );
					}
				}
				distance++;
			}
			distance = 0;
			for ( int x = hero.Position.X; x > hero.Position.X - hero.SightRange; x-- )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, hero.Position.Y + 1 );
				Point tileDown = new Point( x, hero.Position.Y - 1 );
				if ( !map.Map.ContainsKey( new Point( x, hero.Position.Y ) ) || !map.Map[new Point( x, hero.Position.Y )].Info.IsTransparent )
				{
					if ( map.Map.ContainsKey( tileUp ) && !map.Map[tileUp].Info.IsTransparent )
						VisibleTiles.Add( tileUp );
					if ( map.Map.ContainsKey( tileDown ) && !map.Map[tileDown].Info.IsTransparent )
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( map.Map.ContainsKey( tileUp ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileUp].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( map.Map.ContainsKey( tileDown ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileDown].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileDown );
						}
						else
							VisibleTiles.Add( tileDown );
					}
				}
				distance++;
			}
			//to the up
			distance = 0;
			for ( int y = hero.Position.Y; y > hero.Position.Y - hero.SightRange; y-- )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( hero.Position.X + 1, y );
				Point tileLeft = new Point( hero.Position.X - 1, y );
				if ( !map.Map.ContainsKey( new Point( hero.Position.X, y ) ) || !map.Map[new Point( hero.Position.X, y )].Info.IsTransparent )
				{
					if ( map.Map.ContainsKey( tileRight ) && !map.Map[tileRight].Info.IsTransparent )
						VisibleTiles.Add( tileRight );
					if ( map.Map.ContainsKey( tileLeft ) && !map.Map[tileLeft].Info.IsTransparent )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( map.Map.ContainsKey( tileRight ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileRight].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( map.Map.ContainsKey( tileLeft ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileLeft].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileLeft );
						}
						else
							VisibleTiles.Add( tileLeft );
					}

				}
				distance++;
			}
			//to the Down
			distance = 0;
			for ( int y = hero.Position.Y; y < hero.Position.Y + hero.SightRange; y++ )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( hero.Position.X + 1, y );
				Point tileLeft = new Point( hero.Position.X - 1, y );
				if ( !map.Map.ContainsKey( new Point( hero.Position.X, y ) ) || !map.Map[new Point( hero.Position.X, y )].Info.IsTransparent )
				{
					if ( map.Map.ContainsKey( tileRight ) && !map.Map[tileRight].Info.IsTransparent )
						VisibleTiles.Add( tileRight );
					if ( map.Map.ContainsKey( tileLeft ) && !map.Map[tileLeft].Info.IsTransparent )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( map.Map.ContainsKey( tileRight ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileRight].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( map.Map.ContainsKey( tileLeft ) )
					{
						if ( distance >= hideOpeningDistance && map.Map[tileLeft].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileLeft );
						}
						else
							VisibleTiles.Add( tileLeft );
					}

				}
				distance++;
			}

			#endregion

			//Remove duplicate entries from FOV 
			//List<Point> uniq = VisibleTiles.Distinct().ToList();
			//VisibleTiles.Clear();
			//VisibleTiles = uniq;

			//Tiles in new FOV set 
			foreach ( Point tile in VisibleTiles )
			{
				SetInFOV( tile );
			}

			////Render "Monsters" in FOV
			//foreach ( Monster m in actors )
			//{
			//	if ( !map.Map[m.Position].Info.IsInFOV )
			//	{
			//		m.InFov = false;
			//		m.UnRenderFromCell( ActorLayer[m.Position.X, m.Position.Y] );
			//	}
			//	else
			//	{
			//		m.InFov = true;
			//		m.RenderToCell( ActorLayer[m.Position.X, m.Position.Y] );
			//	}
			//}
		}


		internal void Walk( Point tile )
		{
			//Move Hero and open doors when bumped on
			Point destination = hero.Position + tile;
			if ( map.BlocksMove( destination ) == true )
			{
				//if ( map.Map[destination] is IOpenable )
				//{
				//	IOpenable d = map.Map[destination] as IOpenable;
				//	d.Open();

				//	map.Map[destination].RemoveCellFromView( this[destination.X, destination.Y] );
				//	map.Map[destination].RenderToCell( this[destination.X, destination.Y] );
				//	UpdateFov();
				//	}
				MessagesConsole.Instance.PrintMessage( "Blocked Move!" );
				return;
			}
			// Handle render to new pos and unrendering from previous pos
			if ( new Rectangle( 0, 0, Width - 1, Height - 1 ).Contains( destination ) )
			{
				hero.UnRenderFromCell( ActorLayer[hero.Position.X, hero.Position.Y] );
				hero.Position = destination;
				MessagesConsole.Instance.PrintMessage( "You see here: " + map.GetDescription(destination) );
				UpdateFov();
				hero.RenderToCell( ActorLayer[destination.X, destination.Y] );
				// Update Stats screen
				hero.OnChangedEvent();

			}

		}

		private void PlaceHero()
		{
			hero.Position = GetRandomEmpty( 0 );
			StairsUp up = new MapObjects.StairsUp();
			//map.Map.Add( new Point( x, y ), floor );
			map.Map[hero.Position] = up;
			up.RenderToCell( this[hero.Position.X, hero.Position.Y] );
			hero.Appearance.CopyAppearanceTo( ActorLayer[hero.Position.X, hero.Position.Y] );
		}

		private Point GetRandomEmpty( int roomID = -1 )
		{

			Point p;
			int r = roomID;

			if ( roomID == - 1)
				r = random.Next( map.roomRects.Count - 1 );

			Rectangle room = map.roomRects[r];
			System.Console.WriteLine( "room spawn: " + r.ToString() );


			while ( true )
			{
				int x = random.Next( room.X + 1, room.Right - 1 );
				int y = random.Next( room.Y + 1, room.Bottom - 1 );
				p = new Point( x, y );

				foreach ( Monster m in actors )
				{
					if ( m.Position == p )
						break;
				}

				break;
			}

			//System.Console.WriteLine( "AtorLayer:" + ActorLayer[x,y].GlyphIndex);
			System.Console.WriteLine( "spawn point:" + p.ToString() );
			return p;
		}

		private void PlaceMonsters( int amount )
		{
			

			// Position the player somewhere on a walkable square
			for ( int i = 0; i < amount; i++ )
			{
				Monster monster = new Monster();
				actors.Add( monster );
				Point p = GetRandomEmpty();
				monster.Position = p;
				//monster.RenderToCell( ActorLayer[p.X, p.Y] );
				MessagesConsole.Instance.PrintMessage( "Spawned at: " + p.ToString() );
				//Spawn the MF!
			}
		}
	}
}
