using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AntColony.Colony {
	/// <summary>
	/// A.K.A. the ant colony
	/// </summary>
	internal class AntViewModel : INotifyPropertyChanged {
		#region Collection
		private ObservableCollection<Ant> _Ants;
		#endregion
		#region Fields
		/// <summary>
		/// The ant colony itself
		/// </summary>
		public ObservableCollection<Ant> Ants {
			get => this._Ants;
			set {
				this._Ants = value;
				this.OnPropertyChanged(nameof(this.Ants));
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Creates an ant colony
		/// </summary>
		public AntViewModel() {
			this._Ants = new ObservableCollection<Ant>();
		}
		#endregion
		#region Events
		/// <summary>
		/// Event
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// When property changes, call this function
		/// </summary>
		/// <param name="value">Property name</param>
		public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
		#endregion
	}
}
