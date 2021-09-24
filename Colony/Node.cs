﻿namespace AntColony.Colony;
/// <summary>
/// This is a node from the environment
/// </summary>
internal class Node {
	#region Fields
	/// <summary>
	/// Node ID
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// X coordinate
	/// </summary>
	public double X { get; set; }
	/// <summary>
	/// Y coordinate
	/// </summary>
	public double Y { get; set; }
	/// <summary>
	/// IF this cell is marked as discovered
	/// </summary>
	public bool IsDiscovered { get; set; }
	#endregion
}