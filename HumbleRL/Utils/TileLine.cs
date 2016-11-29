using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumbleRL.Utils
{
	class TileLine
	{

		static public List<Point> GetLosLine( Dictionary<Point, MapObjects.MapObjectBase> Map, Point start, Point end, int sightRange, bool lightEndTile = true )
		{
			List<Point> points = new List<Point>();
			int N = diagonalDistance( start, end );
			int count = 0;
			for ( int step = 0; step <= N; step++ )
			{
				float t;
				if ( N == 0 )
					t = 0;
				else
					t = step / ( float ) N;
				Point p = roundPoint( lerpPoint( start, end, t ) );
				if ( !Map.ContainsKey( p ) ||  count  == sightRange) // No tile ie. void besides corridor
					break;
				points.Add( p );
				if ( !Map[p].Info.IsTransparent ) // blocks light but light tile if lightendtile 
				{
					if ( !lightEndTile )
						points.Remove( p );
					break;
				}
				count++;
			}

			return points;
		}

		static public List<Point> GetLine( Point p0, Point p1 )
		{
			List<Point> points = new List<Point>();
			int N = diagonalDistance( p0, p1 );
			for ( int step = 0; step <= N; step++ )
			{
				float t;
				if ( N == 0 )
					t = 0;
				else
					t = step / (float) N;
				Point p = roundPoint( lerpPoint( p0, p1, t ) );
				points.Add( p );
			}
			return points;
		}

		static private Vector2 lerpPoint( Point p0, Point p1, float t )
		{
			return new Vector2( lerp( p0.X, p1.X, t ),
					 lerp( p0.Y, p1.Y, t ) );
		}

		static private float lerp( int start, int end, float t)
		{
			return start + t * ( end - start );
		}

		static private int diagonalDistance( Point p0, Point p1 )
		{
			int dx = p1.X - p0.X;
			int dy = p1.Y - p0.Y;
			return Math.Max( Math.Abs( dx ), Math.Abs( dy ) );
		}

		static private Point roundPoint( Vector2 p )
		{
			return new Point( Convert.ToInt32( p.X ), Convert.ToInt32( p.Y ) );
		}
	}
}
