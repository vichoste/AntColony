using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using AntColony.Models.Main;

using MaterialDesignThemes.Wpf;

namespace PGMLab.ViewModels.Main {
	/// <summary>
	/// View model for the main window
	/// </summary>
	public class MainViewModel : INotifyPropertyChanged {
		#region Attributes
		private PackIconKind? _MaximizeIcon;
		private State _Status;
		private bool _CanOperate;
		private int _AntCount;
		private double _EvaporationRate;
		#endregion
		#region Properties
		/// <summary>
		/// Sets the current maximize icon
		/// </summary>
		public PackIconKind? MaximizeIcon {
			get => this._MaximizeIcon;
			set {
				if (this._MaximizeIcon != value) {
					this._MaximizeIcon = value;
					this.OnPropertyChanged("MaximizeIcon");
				}
			}
		}
		/// <summary>
		/// Current program status
		/// </summary>
		public State Status {
			get => this._Status;
			set {
				if (this._Status != value) {
					this._Status = value;
					this.OnPropertyChanged("Status");
				}
			}
		}
		/// <summary>
		/// Checks if the program can execute the TSP
		/// </summary>
		public bool CanOperate {
			get => this._CanOperate;
			set {
				if (this._CanOperate != value) {
					this._CanOperate = value;
					this.OnPropertyChanged("CanOperate");
				}
			}
		}
		/// <summary>
		/// Ant count for TSP
		/// </summary>
		public int AntCount {
			get => this._AntCount;
			set {
				if (this._AntCount != value) {
					if (value >= 0 && value <= 128) {
						this._AntCount = value;
						this.OnPropertyChanged("AntCount");
					} else if (value < 0) {
						this._AntCount = 0;
						this.OnPropertyChanged("AntCount");
					} else {
						this._AntCount = 128;
						this.OnPropertyChanged("AntCount");
					}
				}
			}
		}
		/// <summary>
		/// Evaporation rate for TSP
		/// </summary>
		public double EvaporationRate {
			get => this._EvaporationRate;
			set {
				if (this._EvaporationRate != value) {
					if (value >= 0 && value <= 1) {
						this._EvaporationRate = value;
						this.OnPropertyChanged("EvaporationRate");
					} else if (value < 0) {
						this._EvaporationRate = 0;
						this.OnPropertyChanged("EvaporationRate");
					} else {
						this._EvaporationRate = 1;
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
			this.MaximizeIcon = PackIconKind.WindowMaximize;
			this.Status = State.Ready;
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