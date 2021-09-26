using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using AntColony.Pathfinding;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int AntMaxStep = 4;
	public const int DefaultAntCount = 2;
	public const int MinAntCount = 2;
	public const int MaxAntCount = 4;
	public static AntNode Clone(AntNode ant) {
		var clone = new AntNode() {
			Id = ant.Id,
			X = ant.X,
			Y = ant.Y
		};
		return clone;
	}
	public static AntNode MoveAnt(AntNode ant, List<Probability> probabilities) {
		var maxProbability = probabilities.MaxBy(p => p.Value);
		var step = RandomNumberGenerator.GetInt32(AntMaxStep + 1);
		if (maxProbability is not null) {
			switch (maxProbability.Direction) {
				case Direction.North:
					ant.Y = ant.Y + step > MaxNodes ? ant.Y - step : ant.Y + step;
					break;
				case Direction.South:
					ant.Y = ant.Y - step < 0 ? ant.Y + step : ant.Y - step;
					break;
				case Direction.East:
					ant.X = ant.X + step > MaxNodes ? ant.X - step : ant.X + step;
					break;
				case Direction.West:
					ant.X = ant.X - step < 0 ? ant.X + step : ant.X - step;
					break;
				case Direction.NorthEast:
					ant.Y = ant.Y + step > MaxNodes ? ant.Y - step : ant.Y + step;
					ant.X = ant.X + step > MaxNodes ? ant.X - step : ant.X + step;
					break;
				case Direction.NorthWest:
					ant.Y = ant.Y + step > MaxNodes ? ant.Y - step : ant.Y + step;
					ant.X = ant.X - step < 0 ? ant.X + step : ant.X - step;
					break;
				case Direction.SouthEast:
					ant.Y = ant.Y - step < 0 ? ant.Y + step : ant.Y - step;
					ant.X = ant.X + step > MaxNodes ? ant.X - step : ant.X + step;
					break;
				case Direction.SouthWest:
					ant.Y = ant.Y - step < 0 ? ant.Y + step : ant.Y - step;
					ant.X = ant.X - step < 0 ? ant.X + step : ant.X - step;
					break;
			}
			return Clone(ant);
		}
		return Clone(ant);
	}
}