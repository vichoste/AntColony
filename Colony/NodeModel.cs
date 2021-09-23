using System.Collections.ObjectModel;

namespace AntColony.Colony {
	/// <summary>
	/// This is a node
	/// </summary>
	internal class NodeModel {
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
		#endregion
		#region Collections
		/// <summary>
		/// Node list
		/// </summary>
		public static ObservableCollection<NodeModel> Nodes = new ObservableCollection<NodeModel>();
		#endregion
	}
}
