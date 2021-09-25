using AntColony.Colony;

namespace AntColony.Pathfinding;
internal class Probability {
	public const int MaxProbability = 99;
	public const int MinProbability = 1;
	public Direction Direction { get; set; }
	public int Value { get; set; }
}
