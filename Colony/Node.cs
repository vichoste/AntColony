using System;

namespace AntColony.Colony;
public abstract class Node {
	public int Id { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public static double Distance(Node firstNode, Node secondNode) => Math.Sqrt(Math.Pow(secondNode.X - firstNode.X, 2) + Math.Pow(secondNode.Y - firstNode.Y, 2));
}