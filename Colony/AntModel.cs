using System.Collections.Generic;

namespace AntColony.Colony {
	/// <summary>
	/// This is an ant
	/// </summary>
	internal class AntModel {
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
		public static List<AntModel> Ants = new List<AntModel>();
		#endregion
	}
}
