using System.Collections.ObjectModel;

namespace AntColony.Ant {
	/// <summary>
	/// A.K.A. the ant colony
	/// </summary>
	internal class AntViewModel {
		#region Collection
		private readonly ObservableCollection<AntModel> Ants;
		#endregion
		#region Constructors
		/// <summary>
		/// Creates an ant colony
		/// </summary>
		public AntViewModel() {

		}
		#endregion
	}
}
