using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

namespace AntColony.Main {
	/// <summary>
	/// View model for the main window
	/// </summary>
	internal class MainViewModel : INotifyPropertyChanged {
		#region Model
		private readonly MainModel _MainModel;
		#endregion
		#region Fields
		/// <summary>
		/// Sets the current maximize icon
		/// </summary>
		public PackIconKind? MaximizeIcon {
			get => this._MainModel.MaximizeIcon;
			set {
				if (this._MainModel.MaximizeIcon != value) {
					this._MainModel.MaximizeIcon = value;
					this.OnPropertyChanged("MaximizeIcon");
				}
			}
		}
		/// <summary>
		/// Current program status
		/// </summary>
		public Status Status {
			get => this._MainModel.Status;
			set {
				if (this._MainModel.Status != value) {
					this._MainModel.Status = value;
					this.OnPropertyChanged("Status");
				}
			}
		}
		/// <summary>
		/// Checks if the program can execute the TSP
		/// </summary>
		public bool CanOperate {
			get => this._MainModel.CanOperate;
			set {
				if (this._MainModel.CanOperate != value) {
					this._MainModel.CanOperate = value;
					this.OnPropertyChanged("CanOperate");
				}
			}
		}
		/// <summary>
		/// Ant count for TSP
		/// </summary>
		public int AntCount {
			get => this._MainModel.AntCount;
			set {
				if (this._MainModel.AntCount != value) {
					if (value >= MainModel.MinAntCount && value <= MainModel.MaxAntCount) {
						this._MainModel.AntCount = value;
						this.OnPropertyChanged("AntCount");
					} else if (value < 0) {
						this._MainModel.AntCount = MainModel.MinAntCount;
						this.OnPropertyChanged("AntCount");
					} else {
						this._MainModel.AntCount = MainModel.MaxAntCount;
						this.OnPropertyChanged("AntCount");
					}
				}
			}
		}
		/// <summary>
		/// Evaporation rate for TSP
		/// </summary>
		public double EvaporationRate {
			get => this._MainModel.EvaporationRate;
			set {
				if (this._MainModel.EvaporationRate != value) {
					if (value >= MainModel.MinEvaporationRate && value <= MainModel.MaxEvaporationRate) {
						this._MainModel.EvaporationRate = value;
						this.OnPropertyChanged("EvaporationRate");
					} else if (value < 0) {
						this._MainModel.EvaporationRate = MainModel.MinEvaporationRate;
						this.OnPropertyChanged("EvaporationRate");
					} else {
						this._MainModel.EvaporationRate = MainModel.MaxEvaporationRate;
						this.OnPropertyChanged("EvaporationRate");
					}
				}
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Creates the view model
		/// </summary>
		public MainViewModel() {
			this.OpenCommand = new MainCommmand(new Action<object>(ExecuteOpenCommand));
			this._MainModel = new MainModel() {
				MaximizeIcon = PackIconKind.WindowMaximize,
				Status = Status.Ready,
				AntCount = 4,
				EvaporationRate = .5
			};
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
		#region Command properties
		/// <summary>
		/// Test command
		/// </summary>
		public ICommand OpenCommand { get; set; }
		#endregion
		#region Command methods
		/// <summary>
		/// Test command
		/// </summary>
		/// <param name="obj">Object to manipulate</param>
		public static void ExecuteOpenCommand(object obj) => _ = MessageBox.Show($"Made by Vicente \"vichoste\" Calderón");
		#endregion
	}
}