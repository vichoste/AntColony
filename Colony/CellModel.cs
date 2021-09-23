namespace AntColony.Colony {
	/// <summary>
	/// This is a cell from the environment
	/// </summary>
	internal class CellModel {
		#region Fields
		/// <summary>
		/// IF this cell is marked as a node
		/// </summary>
		public bool IsNode { get; set; }
		#endregion
		#region Constants
		public const int MaxCells = 512;
		#endregion
		#region Collections
		/// <summary>
		/// Node list
		/// </summary>
		public static CellModel[,] Cells = new CellModel[MaxCells, MaxCells];
		#endregion
	}
}
