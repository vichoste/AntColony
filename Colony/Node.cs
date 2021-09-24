using System;

namespace AntColony.Colony;
/// <summary>
/// This is a node from the environment
/// </summary>
internal abstract class Node {
	#region Fields
	/// <summary>
	/// Node ID
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// X coordinate
	/// </summary>
	public int X { get; set; }
	/// <summary>
	/// Y coordinate
	/// </summary>
	public int Y { get; set; }
	/// <summary>
	/// Node type
	/// </summary>
	public string? ObservableNodeType { get; protected set; }
	#endregion
	#region Constants
	/// <summary>
	/// Maximum amount of nodes
	/// </summary>
	public const int MaxNodes = 300;
	#endregion
	#region Static methods
	/// <summary>
	/// Calculates distance between two nodes
	/// </summary>
	/// <param name="firstNode">First node</param>
	/// <param name="secondNode">Second node</param>
	/// <returns>Euclidian scalar distance</returns>
	public static double Distance(Node firstNode, Node secondNode) => Math.Sqrt(Math.Pow(secondNode.X - firstNode.X, 2) + Math.Pow(secondNode.Y - firstNode.Y, 2));
	#endregion
}