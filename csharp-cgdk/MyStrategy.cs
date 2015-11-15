using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
	public sealed class MyStrategy : IStrategy
	{
		public void Move (Car self, World world, Game game, Move move)
		{
			double nextWaypointX = (self.NextWaypointX + 0.5) * game.TrackTileSize;
			double nextWaypointY = (self.NextWaypointY + 0.5) * game.TrackTileSize;

			Console.WriteLine (self.NextWaypointX + " " + self.NextWaypointY);

			double cornerTileOffset = 0.25D * game.TrackTileSize;

			switch (world.TilesXY [self.NextWaypointX] [self.NextWaypointY]) {
			case TileType.LeftTopCorner: 
				nextWaypointX += cornerTileOffset;
				nextWaypointY += cornerTileOffset;
				break;
			case TileType.RightTopCorner:
				nextWaypointX -= cornerTileOffset;
				nextWaypointY += cornerTileOffset;
				break;
			case TileType.LeftBottomCorner:
				nextWaypointX += cornerTileOffset;
				nextWaypointY -= cornerTileOffset;
				break;
			case TileType.RightBottomCorner:
				nextWaypointX -= cornerTileOffset;
				nextWaypointY -= cornerTileOffset;
				break;
            
			}

			double angleToWaypoint = self.GetAngleTo (nextWaypointX, nextWaypointY);
			double speedModule = hypot (self.SpeedX, self.SpeedY);

			move.WheelTurn = (angleToWaypoint * 32.0D / System.Math.PI);
			move.EnginePower = (1.0D);
			if (speedModule > 30)
				move.EnginePower = 0;

			if (speedModule * speedModule * System.Math.Abs (angleToWaypoint)
			    > 2.5D * 2.5D * System.Math.PI) {

				move.EnginePower = (0.0D);
				if (speedModule > 10) {
					move.IsBrake = true;
					if (angleToWaypoint > 0)
						move.WheelTurn = 1;
					else
						move.WheelTurn = -1;
				}
			}

			move.IsThrowProjectile = true;
			move.IsSpillOil = true;
		}

		double hypot (double x, double y)
		{
			return System.Math.Sqrt (x * x + y * y);
		}

	}
}
    
