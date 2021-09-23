using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

using AntColony.Colony;

using AsyncAwaitBestPractices.MVVM;

using MaterialDesignThemes.Wpf;

using Microsoft.Win32;

namespace AntColony.Main;
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
				this.OnPropertyChanged(nameof(this.MaximizeIcon));
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
				this.OnPropertyChanged(nameof(this.Status));
			}
		}
	}
	/// <summary>
	/// Border margin
	/// </summary>>
	public int BorderMargin {
		get => this._MainModel.BorderMargin;
		set {
			if (this._MainModel.BorderMargin != value) {
				this._MainModel.BorderMargin = value;
				this.OnPropertyChanged(nameof(this.BorderMargin));
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
				this.OnPropertyChanged(nameof(this.CanOperate));
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
				this._MainModel.AntCount = value >= MainModel.MinAntCount && value <= MainModel.MaxAntCount
					? value
					: value < 0 ? MainModel.MinAntCount : MainModel.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
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
				this._MainModel.EvaporationRate = value >= MainModel.MinEvaporationRate && value <= MainModel.MaxEvaporationRate
					? value
					: value < 0 ? MainModel.MinEvaporationRate : MainModel.MaxEvaporationRate;
				this.OnPropertyChanged(nameof(this.EvaporationRate));
			}
		}
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the view model
	/// </summary>
	public MainViewModel() {
		for (var i = 0; i < CellModel.MaxCells; i++) {
			for (var j = 0; j < CellModel.MaxCells; j++) {
				CellModel.Cells[i, j] = new CellModel();
			}
		}
		this.OpenTspFileCommand = new AsyncCommand(async () => {
			this.Status = Status.Opening;
			var openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true) {
				// Allow only TSP files
				if (!openFileDialog.FileName.ToLower().EndsWith(".tsp")) {
					_ = MessageBox.Show("Can't open file. Only *.tsp files are allowed!");
					return;
				}
				// Read all lines
				var lines = await AsyncFileReader.ReadAllLinesAsync(openFileDialog.FileName, Encoding.UTF8);
				if (!lines[4].EndsWith("EUC_2D")) { // Only allow Euclidian 2D, because I don't have time for other graph types
					_ = MessageBox.Show("Can't open file. Only EUC_2D graphs are allowed!");
					return;
				}
				if (!lines[5].Equals("NODE_COORD_SECTION")) {
					_ = MessageBox.Show("Can't open file. Expected \"NODE_COORD_SECTION : \" : at line 6!");
					return;
				}
				for (var i = 6; i < lines.Length - 1; i++) {
					try {
						// Parse integers (https://stackoverflow.com/questions/4961675/select-parsed-int-if-string-was-parseable-to-int)
						var splitted = lines[i].Split(' ').Select(str => {
							var success = int.TryParse(str, out var value);
							return (value, success);
						}).Where(pair => pair.success).Select(pair => pair.value).ToList();
						if (splitted[1] > 512 || splitted[2] > 512) {
							_ = MessageBox.Show("Can't open file. For simplicity, only distances below 512!");
							return;
						}
						CellModel.Cells[splitted[1], splitted[2]].IsNode = true; // Mark this as a node
					} catch (Exception) {
						_ = MessageBox.Show("Can't open file. For simplicity, only integers!");
						return;
					}
				}
			}
			this.Status = Status.Ready;
		});
		this._MainModel = new MainModel() {
			MaximizeIcon = PackIconKind.WindowMaximize,
			Status = Status.Ready,
			BorderMargin = 8,
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
	public void OnPropertyChanged(string value) {
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
	}
	#endregion
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public IAsyncCommand OpenTspFileCommand { get; set; }
	#endregion
}