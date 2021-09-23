using System.Collections.Generic;
using System.Windows.Documents;

namespace AntColony.Colony {
	/// <summary>
	/// This is an ant
	/// </summary>
	internal class Ant {
		#region Fields
		/// <summary>
		/// Ant ID
		/// </summary>
		public int Id { get; set; }
		#endregion
		#region Collections
		/// <summary>
		/// The ant colony itself
		/// </summary>
		public static List<Ant> Ants = new List<Ant>();
		#endregion
	}
}
